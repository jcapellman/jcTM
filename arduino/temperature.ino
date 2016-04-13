#include <fstream>
#include <sstream>
#include <string>

#include <SPI.h>
#include <WiFi.h>

#include "rgb_lcd.h"

WiFiClient client;
rgb_lcd lcd;

std::string wifi_ssid;
std::string wifi_password;
std::string webapi_address;

bool enableLCD = false;
bool enableWIFI = true;

// Using Blue Background - may make it configurable
const int colorG = 0;
const int colorR = 0; 
const int colorB = 255;

void readConfig() {
  std::string line;
  
  std::ifstream infile("config");
   
  while (std::getline(infile, line)) {
    std::istringstream iss(line);
    std::string key, value;
    
    if (!(iss >> key >> value)) {
      break; 
    }

    if (key == "wifi_ssid") {
      wifi_ssid = value;
    } else if (key == "wifi_password") {
      wifi_password = value;
    } else if (key == "enableLCD") {
      enableLCD = (value == "true");
    } else if (key == "webapi_address") {
      webapi_address = value;
    }
  }

  infile.close();
  
  enableWIFI = !wifi_ssid.empty();
}

void setup() {
  Serial.begin(9600);
  while (!Serial) {
    ;
  }

  readConfig();
  
  if (enableLCD) {
    lcd.begin(16, 2);
    lcd.setRGB(colorR, colorG, colorB);
  } else {
    Serial.println("LCD disabled");
  }

  if (!enableWIFI) {
    Serial.println("WiFi disabled");
    return;
  }
  
  int status = WL_IDLE_STATUS;

  while (status != WL_CONNECTED) {
    Serial.print("Attempting to connect to SSID: ");
    Serial.println(wifi_ssid.c_str());
    
    status = WiFi.begin((char*)wifi_ssid.c_str(), (char*)wifi_password.c_str());

    delay(10000);
  }

  Serial.println("Connected to wifi");
}

float getTemperature() {
  int analogReading = analogRead(0);
   
  float resistance = (float)(1023 - analogReading) * 10000 / analogReading;
  float temperature = 1 / (log(resistance / 10000)/3975 + 1 / 298.15) - 273.15;
  
  return ((9.0/5.0) * temperature) + 32.0;
}

void loop() {
  float temperature = getTemperature();
  
  delay(1000);

  if (enableLCD) {
    lcd.clear();
    lcd.print("Temp(F) is ");
    lcd.println(temperature);
  }

  if (!enableWIFI) {
    delay(2000);
    return;
  }
  
  char buf[100];
  sprintf(buf, "GET /api/Temperature?temperature=%f HTTP/1.0", temperature);

  if (client.connected()) {
    client.stop();
  }

  if (client.connect(webapi_address.c_str(), 80)) {
    Serial.println(buf);
    client.println(buf);
    client.println(("Host: " + webapi_address).c_str());
    client.println("Connection: close");
    client.println();

     client.stop();
  }

  delay(5000);
}

#include "C:\Intel\arduino-1.6.5\libraries\rgb_lcd.h"

rgb_lcd lcd;

 
const int colorG = 0;
const int colorR = 0; 
const int colorB = 255;

void setup() {
  // put your setup code here, to run once:
  lcd.begin(16, 2);
  lcd.setRGB(colorR, colorG, colorB);
    lcd.print("Temperature is");
}

void loop() {
  
}

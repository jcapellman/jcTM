# jcTM
ioT Temperature Monitor with a C#/SQL Backend

###Intro<br/>
What started out as a simple "get into IoT" turned into a game charger for my own home automation.  Questions of how hot does your house really get in different sections of your house especially on multi-floor scenarios like what I live in (basement being the coolest and the top floor being the hottest).  Over time I'll be focusing on not only the temperature aspect, but also humidity and barometric pressure.

###Project Structure<br/>
You'll notice above there are three main folders.  As the names imply, the first being arduino.  This is code that works on my Intel Edison board which also supports the Grove RGB LCD if you wanted a more "traditional" thermometer.  The 2ond folder is the C# projects (WebAPI, UWP, UWP IoT, Data Layer and PCL).  This code is for Windows 10 (Mobile and Desktop), the Web API Service and the UWP IoT project that I run on a Raspberry Pi 3 running Windows 10 IoT.  Lastly the SQL folder as the name implies has all of the scripts needed to generate your own SQL database for storage of temperatures.

###Project Goals<br/>
As mentioned above I plan to add Humidity and Barometric pressure in the comming week or two in addition to adding security and more reporting/trending to the platform.

###Questions/Concerns<br/>
If anyone has any suggestions/comments/questions please email me @ jarred at jarredcapellman dot com.

// Visual Micro is in vMicro>General>Tutorial Mode
// 
/*
	Name:       ArdTestProj.ino
	Created:	2024. 02. 10. 22:27:00
	Author:     CSABA\user
*/

// Define User Types below here or use a .h file
//#include <SoftwareSerial.h>


// Define Function Prototypes that use User Types below here or use a .h file
//


// Define Functions below here or use other .ino or cpp files
//

const int sensorPin0 = A0;
const int sensorPin1 = A1;
const float baselineTemp = 25.0;
const int DigitalOutput1 = 2;
const int DigitalOutput2 = 3;
const int DigitalInput1 = 7;
const int DigitalInput2 = 8;
int buttonstate;
char serialLedData = 0;
int ledData = 0;
int sensorValues[2];
int digitalInputValues[2];
int digitalOutputValues[2];
int digitalOutputPins[2];
String serialData;
String answare;


void setup()
{
	Serial.setTimeout(100);
	Serial.begin(9600);
	//Inputs
	pinMode(DigitalInput1, INPUT);
	pinMode(DigitalInput2, INPUT);
	//OutPuts
	pinMode(DigitalOutput1, OUTPUT);
	pinMode(DigitalOutput2, OUTPUT);
	digitalWrite(DigitalOutput1, LOW);
	digitalWrite(DigitalOutput2, LOW);
	digitalOutputPins[0] = DigitalOutput1;
	digitalOutputPins[1] = DigitalOutput2;
}

void loop()
{
	sensorValues[0] = analogRead(sensorPin0);
	sensorValues[1] = analogRead(sensorPin1);
	digitalInputValues[0] = digitalRead(DigitalInput1);
	digitalInputValues[1] = digitalRead(DigitalInput2);
	digitalOutputValues[0] = digitalRead(digitalOutputPins[0]);
	digitalOutputValues[1] = digitalRead(digitalOutputPins[1]);

	if (Serial.available())
	{
		//(RD0;-BUTTON, RS0-TEMP;RS1-FOTORESISTOR;WO01-LEDWRITEON; WO00-LEDWRITEOFF; H;-HelloArduino)+@ caracter 
		serialData = Serial.readStringUntil('@');
		if (serialData != NULL)
		{
			//Serial.println("Message received : " + serialData);
			answare = "";
			String commandMessage;
			//Split String
			for (int i = 0; i < serialData.length(); i++)
			{
				if (serialData[i] == ';')
				{
					char command = commandMessage.charAt(0);
					char pinType = commandMessage.charAt(1);
					int pinNumber = commandMessage.charAt(2) - '0';
					int pinValue = 0;
					if (commandMessage.length() >= 4)
					{
						pinValue = commandMessage.charAt(3) - '0';
					}

					//command explanatory
					switch (command)
					{
					case 'R':
					{
						if (pinType == 'I')
						{
							answare += (String)digitalInputValues[pinNumber] + ";";
						}
						if (pinType == 'O')
						{
							answare += (String)digitalOutputValues[pinNumber] + ";";
						}
						else if (pinType == 'S')
						{
							if (pinNumber == 0)
							{
								answare += (String)(ConvertToCelsius(sensorValues[pinNumber])) + ";";
							}
							else if (pinNumber == 1)
							{
								answare += (String)sensorValues[pinNumber] + ";";
							}
						}
					}break;

					case 'W':
					{
						if (pinType == 'O')
						{
							digitalWrite(digitalOutputPins[pinNumber], pinValue);
							answare += (String)pinValue + ";";
						}

					}break;

					case 'H':
					{
						answare += "HelloArduino;";
					}break;

					default:
						break;
					}
					commandMessage = "";
				}
				else
				{
					commandMessage += serialData[i];
				}
			}
			Serial.print(answare);
			Serial.println();
		}
	}
}

float ConvertToCelsius(int SensoreVal)
{
	float voltage = (SensoreVal / 1024.0) * 5.0;
	float temperature = (voltage - .5) * 100;
	return temperature;
}

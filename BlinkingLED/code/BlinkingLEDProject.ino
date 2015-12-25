#include <DigiUSB.h>

#define yellowLED 0
#define greenLED  2
#define redLED 1

typedef enum{
  LED_OFF,
  LED_ON,
  LEDBlinking_ON,
  LEDBlinking_OFF

} State;


typedef enum{
  LightUpAll,
  LightOffAll,
  RunningLight,
  BlinkAll
} Presets;


typedef struct{
  int LED;
  int state;
  int OnSignal;
  int OffSignal;
  int BlinkSignal;
  unsigned long interval;
  unsigned long previousMillis;
} LedData;

LedData red,yellow,green;
int incomingSignal;
int previousSignal = -1;
int presetFreq;
int freq;

void setup() {
  pinMode(yellowLED, OUTPUT);
  pinMode(redLED, OUTPUT);
  pinMode(greenLED, OUTPUT);
  ledInitData(&red,redLED );
  ledInitData(&green, greenLED);
  ledInitData(&yellow, yellowLED);
  DigiUSB.begin();
  DigiUSB.println("USB is ready");
}


int isTimerExpires(int timeConstraint,LedData *data)
{
  unsigned long currentMillis = millis();

  if (currentMillis - data->previousMillis >= data->interval) 
  {
    data->previousMillis = currentMillis;
    return 1;
  }
  else
    return 0;
}

void ledInitData(LedData *data, int ledSelection)
{
  data->state = LED_OFF;
  data->LED = ledSelection;
  data->previousMillis = 0;
  data->OnSignal = 0;
  data->OffSignal = 0;
  data->BlinkSignal = 0;
  data->interval = 0;
}

void changeOnSignal(LedData *data, int value)
{
  data->OnSignal = value;
  data->OffSignal = 0;
  data->BlinkSignal = 0;
}

void changeOffSignal(LedData *data, int value)
{
  data->OffSignal = value;
  data->OnSignal = 0;
  data->BlinkSignal = 0;
}

void changeBlinkSignal(LedData *data, int value)
{
  data->BlinkSignal = value;
  data->OffSignal = 0;
  data->OnSignal = 0;
}



void loadPreset(Presets mode, LedData *red, LedData *yellow, LedData *green, int interval)
{
  if(mode == LightUpAll)
    LightUpAllPreset(red, yellow, green);
  else if(mode == LightOffAll)
    LightOffAllPreset(red, yellow, green);
  else if(mode == RunningLight)
    RunningLightPreset(red, yellow, green);
  else if(mode == BlinkAll)
    BlinkAllPreset(red, yellow, green, interval);
}


void RunningLightPreset(LedData *red, LedData *yellow, LedData *green)
{
  red->OffSignal = 0;
  red->BlinkSignal = 1;
  red->OnSignal = 0;
  yellow->OffSignal = 0;
  yellow->BlinkSignal = 1;
  yellow->OnSignal = 0;
  green->OffSignal = 0;
  green->BlinkSignal = 1;
  green->OnSignal = 0;

  //set the blink freq
  green->interval = 1600;
  red->interval = 800;
  yellow->interval = 400; 
}


void BlinkAllPreset(LedData *red, LedData *yellow, LedData *green, int interval)
{
  red->OffSignal = 0;
  red->BlinkSignal = 1;
  red->OnSignal = 0;
  yellow->OffSignal = 0;
  yellow->BlinkSignal = 1;
  yellow->OnSignal = 0;
  green->OffSignal = 0;
  green->BlinkSignal = 1;
  green->OnSignal = 0;

  //set the blink freq
  green->interval = interval*4;
  red->interval = interval*4;
  yellow->interval = interval*4; 
}


void LightOffAllPreset(LedData *red, LedData *yellow, LedData *green)
{
  red->OffSignal = 1;
  red->BlinkSignal = 0;
  red->OnSignal = 0;
  yellow->OffSignal = 1;
  yellow->BlinkSignal = 0;
  yellow->OnSignal = 0;
  green->OffSignal = 1;
  green->BlinkSignal = 0;
  green->OnSignal = 0;
}

void LightUpAllPreset(LedData *red, LedData *yellow, LedData *green)
{
  red->OnSignal = 1;
  red->OffSignal = 0;
  red->BlinkSignal = 0;
  yellow->OnSignal = 1;
  yellow->OffSignal = 0;
  yellow->BlinkSignal = 0;
  green->OnSignal = 1;
  green->OffSignal = 0;
  green->BlinkSignal = 0;
}


void ledSM(LedData *data)
{
  switch(data->state)
  {
    case LED_OFF : digitalWrite(data->LED, LOW);
                   if(data->OffSignal == 0 && data->BlinkSignal == 0 && data->OnSignal == 0)
                    data->state = LED_OFF;
                   else if(data->OffSignal == 1)    
                      data->state = LED_OFF;
                   else if(data->OnSignal == 1)
                      data->state = LED_ON;
                   else if(data->BlinkSignal == 1)
                      data->state = LEDBlinking_ON;
                   break;
                   
    case LEDBlinking_ON :   digitalWrite(data->LED, HIGH);
                            if(data->BlinkSignal == 1)
                            {
                              if( isTimerExpires(data->interval,data) )
                                data->state = LEDBlinking_OFF;
                              else
                                data->state = LEDBlinking_ON;
                            }
                            else if(data->OnSignal == 1)
                                data->state = LEDBlinking_ON;
                            else if(data->OffSignal == 1)
                                data->state = LED_OFF;
                            break;
                            
    case LEDBlinking_OFF :  digitalWrite(data->LED, LOW);
                            if(data->OnSignal == 1)
                              data->state = LED_ON;
                            else if(data->BlinkSignal == 1)
                            {
                              if( isTimerExpires(data->interval,data) )
                                data->state = LEDBlinking_ON;
                              else
                                data->state = LEDBlinking_OFF;
                            }
                            else if(data->OffSignal == 1)
                              data->state = LED_OFF;                            
                            break;
                            
    case LED_ON :   digitalWrite(data->LED, HIGH);
                    if(data->BlinkSignal == 1)
                        data->state = LEDBlinking_ON;
                    else if(data->OnSignal == 1)
                        data->state = LED_ON;
                    else if(data->OffSignal == 1)
                        data->state = LED_OFF;                       
                    break;
                               
  }
}

void loop() 
{
  /*    Blink   On    Off    getFreq   setFreq
   * R   0       1     2       9         17
   * Y   3       4     5       10        18
   * G   6       7     8       11        19
   */
  if(DigiUSB.available() > 0)
  {
    incomingSignal = DigiUSB.read();
    if(previousSignal == 15)    // means the set preset freq button has clicked
    {
       presetFreq = incomingSignal;
    }
    else if(previousSignal == 9)    // means the set red freq button has clicked
    {
       red.interval = incomingSignal * 4;
    }
    else if(previousSignal == 10)    // means the set yellow freq button has clicked
    {
       yellow.interval = incomingSignal * 4;
    }
    else if(previousSignal == 11)    // means the set green freq button has clicked
    {
       green.interval = incomingSignal * 4;
    }
    else
    {
      if(incomingSignal == 0 || incomingSignal == 1 || incomingSignal == 2)
      {
        if(incomingSignal == 0)
          changeBlinkSignal(&red, 1);
        else if(incomingSignal == 1)
          changeOnSignal(&red, 1);
        else if(incomingSignal == 2)
          changeOffSignal(&red, 1);  
      }
      else if(incomingSignal == 3 || incomingSignal == 4 || incomingSignal == 5)
      {
        if(incomingSignal == 3)
          changeBlinkSignal(&yellow, 1);
        else if(incomingSignal == 4)
          changeOnSignal(&yellow, 1);
        else if(incomingSignal == 5)
          changeOffSignal(&yellow, 1);    
      }
      else if(incomingSignal == 6 || incomingSignal == 7 || incomingSignal == 8)
      {
        if(incomingSignal == 6)
          changeBlinkSignal(&green, 1);
        else if(incomingSignal == 7)
          changeOnSignal(&green, 1);
        else if(incomingSignal == 8)
          changeOffSignal(&green, 1);
      }
      else if(incomingSignal == 12 || incomingSignal == 13 || incomingSignal == 14 || incomingSignal == 16)
      {
        if(incomingSignal == 12)
          loadPreset(LightUpAll,&red,&yellow,&green,0);
        else if(incomingSignal == 13)
          loadPreset(LightOffAll,&red,&yellow,&green,0);
        else if(incomingSignal == 14)
          loadPreset(RunningLight,&red,&yellow,&green,0);
        else if(incomingSignal == 16)
          loadPreset(BlinkAll,&red,&yellow,&green,presetFreq);
      }
    }
   }

    ledSM(&red);
    ledSM(&yellow);
    ledSM(&green);
    previousSignal = incomingSignal;
    DigiUSB.delay(10);
}



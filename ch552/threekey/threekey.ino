#ifndef USER_USB_RAM
#error "This example needs to be compiled with a USER USB setting"
#error "Tools--> USB Settings--> USER CODE w/148 USB Ram"
#endif

#include "src/CDC_KEYBOARD/USBCDC.h"
#include "src/CDC_KEYBOARD/USBHIDKeyboard.h"

//CH552 io
#define BUTTON1_PIN 15
#define BUTTON2_PIN 16
#define BUTTON3_PIN 14

bool button1PressPrev = false;
bool button2PressPrev = false;
bool button3PressPrev = false;
//ctrl
char key1=0x80;
//c
char key2='c';
//v
char key3='v';
// is mediakey?
byte key1func=0x00;
byte key2func=0x00;
byte key3func=0x00;

char key1f=0xff;
char key2f=0xff;
char key3f=0xff;

__xdata char custStr1[30]={0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff};
__xdata char custStr2[30]={0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff};
__xdata char custStr3[30]={0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff};
//keymap[0] start label
//keymap[1] key1
//keymap[2] key2
//keymap[3] key3
//keymap[4] key1func
//keymap[5] key2func
//keymap[6] key3func
//keymap[7] key1 f
//keymap[8] key2 f
//keymap[9] key3 f
//keymap[10]-[39] custom str1
//keymap[40]-[69] custom str2
//keymap[70]-[99] custom str3
//keymap[100] end label
__xdata char keymap[101]={0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,
                  0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,
                  0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,
                  0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,
                  0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,
                  0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,
                  0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,
                  0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,
                  0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,
                  0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,
                  0xff
                  };
int label=0;
void setup() {
  USBInit();
  pinMode(BUTTON1_PIN, INPUT_PULLUP);
  pinMode(BUTTON2_PIN, INPUT_PULLUP);
  pinMode(BUTTON3_PIN, INPUT_PULLUP);
  //read from eeprom
  uint8_t keymap1 = eeprom_read_byte(0);
  uint8_t keymap2 = eeprom_read_byte(1);
  uint8_t keymap3 = eeprom_read_byte(2);
  uint8_t eep3 = eeprom_read_byte(3);
  uint8_t eep4 = eeprom_read_byte(4);
  uint8_t eep5 = eeprom_read_byte(5);
  uint8_t eep6 = eeprom_read_byte(6);
  uint8_t eep7 = eeprom_read_byte(7);
  uint8_t eep8 = eeprom_read_byte(8);
  if(keymap1!=0xff){
     key1=keymap1;
  }
  if(keymap2!=0xff){
     key2=keymap2;
  }
  if(keymap3!=0xff){
     key3=keymap3;
  }
   if(eep3!=0xff){
     key1func=eep3;
  }
  if(eep4!=0xff){
     key2func=eep4;
  }
  if(eep5!=0xff){
     key3func=eep5;
  }
  if(eep6!=0xff){
     key1f=eep6;
  }
  if(eep7!=0xff){
     key2f=eep7;
  }
  if(eep8!=0xff){
     key3f=eep8;
  }
  for(int i=0;i<30;i++){
   custStr1[i] = eeprom_read_byte(9+i);
   custStr2[i] = eeprom_read_byte(39+i);
   custStr3[i] = eeprom_read_byte(69+i);
  }
  
}

void loop() {
	//read serial msg
  while(USBSerial_available()) {
    char serialChar = USBSerial_read();
    if ((serialChar != '\n') && (serialChar != '\r') ) {
      if(label>100){
        label=0;
        }
      if(label==0 && serialChar!=0x02){
        continue;
      }
      if(label==1 && serialChar==0x03){
      	USBSerial_println(key1,HEX);
        USBSerial_println(key2,HEX);
        USBSerial_println(key3,HEX);
        USBSerial_println(key1func,HEX);
        USBSerial_println(key2func,HEX);
        USBSerial_println(key3func,HEX);
        USBSerial_println(key1f,HEX);
        USBSerial_println(key2f,HEX);
        USBSerial_println(key3f,HEX);
        for(int i=0;i<30;i++){
            if(custStr1[i]==0xff){
              break;
              }
             USBSerial_print_c(custStr1[i]);  
         }
         USBSerial_println();
         for(int i=0;i<30;i++){
            if(custStr2[i]==0xff){
              break;
              }
             USBSerial_print_c(custStr2[i]);  
         }
           USBSerial_println();
         for(int i=0;i<30;i++){
            if(custStr3[i]==0xff){
              break;
              }
             USBSerial_print_c(custStr3[i]);  
         }   
           USBSerial_println();
        USBSerial_flush();
      	 label=0;
      	 continue;
      }
      keymap[label]=serialChar;
      label++;
    } 
  }
  if(label==101){
      label=0;
      if(keymap[0]==0x02 && keymap[100]==0x03){
        USBSerial_print_s("1");
        USBSerial_flush();
        for(int i=0;i<98;i++){
        eeprom_write_byte(i, keymap[i+1]);
        }
        key1=keymap[1];
        key2=keymap[2];
        key3=keymap[3];
        key1func=keymap[4];
        key2func=keymap[5];
        key3func=keymap[6];
        key1f=keymap[7];
        key2f=keymap[8];
        key3f=keymap[9];
        for(int i=0;i<30;i++){
        custStr1[i]=keymap[10+i];
        custStr2[i]=keymap[40+i];
        custStr3[i]=keymap[70+i];
        }
       
      }
  }
  bool button1Press = !digitalRead(BUTTON1_PIN);
  if (button1PressPrev != button1Press) {
    button1PressPrev = button1Press;
    if (button1Press) {
      if(key1func==0x00){  //normal
        Keyboard_press(key1);
      }
      else if(key1func==0x01){ //media
      	if(key1f!=0xff){
      		Consumer_press((key1f<<8)+key1);
      	}else{
        Consumer_press(key1);
     		}
      }
      else if(key1func==0x02){ //mix
         Keyboard_press(key1f);
         Keyboard_press(key1);
      }
      else if(key1func==0x03){ //string
         for(int i=0;i<30;i++){
            if(custStr1[i]==0xff){
              break;}
             Keyboard_write(custStr1[i]);
         }
      }   


    }
    else {
       if(key1func==0x00){  //normal
        Keyboard_release(key1);
      }
      else if(key1func==0x01){ //media
        if(key1f!=0xff){
          Consumer_release((key1f<<8)+key1);
        }
        Consumer_release(key1);
      }
      else if(key1func==0x02){ //mix
         Keyboard_release(key1);
         Keyboard_release(key1f);
      }    
    }
  }
  bool button2Press = !digitalRead(BUTTON2_PIN);
  if (button2PressPrev != button2Press) 
  {
    button2PressPrev = button2Press;
    if (button2Press) {
         if(key2func==0x00){  //normal
        Keyboard_press(key2);
      }
      else if(key2func==0x01){ //media
        if(key2f!=0xff){
          Consumer_press((key2f<<8)+key2);
        }
        Consumer_press(key2);
      }
      else if(key2func==0x02){ //mix
         Keyboard_press(key2f);
         Keyboard_press(key2);
      }
      else if(key2func==0x03){ //string
         for(int i=0;i<30;i++){
            if(custStr2[i]==0xff){
              break;}
             Keyboard_write(custStr2[i]);
         }
      }   
    } 
    else {
     if(key2func==0x00){  //normal
        Keyboard_release(key2);
      }
      else if(key2func==0x01){ //media
          if(key2f!=0xff){
          Consumer_release((key2f<<8)+key2);
        }
        Consumer_release(key2);
      }
      else if(key2func==0x02){ //mix
         Keyboard_release(key2);
         Keyboard_release(key2f);
      }    
    }
  }

  bool button3Press = !digitalRead(BUTTON3_PIN);
  if (button3PressPrev != button3Press) 
  {
    button3PressPrev = button3Press;
    if (button3Press) {
        if(key3func==0x00){  //normal
        Keyboard_press(key3);
      }
      else if(key3func==0x01){ //media
        if(key3f!=0xff){
          Consumer_press((key3f<<8)+key3);
        }
        Consumer_press(key3);
      }
      else if(key3func==0x02){ //mix
         Keyboard_press(key3f);
         Keyboard_press(key3);
      }
      else if(key3func==0x03){ //string
         for(int i=0;i<30;i++){
            if(custStr3[i]==0xff){
              break;}
             Keyboard_write(custStr3[i]);
         }
      }   
    } 
    else {
     if(key3func==0x00){  //normal
        Keyboard_release(key3);
      }
      else if(key3func==0x01){ //media
          if(key3f!=0xff){
          Consumer_release((key3f<<8)+key3);
        }
        Consumer_release(key3);
      }
      else if(key3func==0x02){ //mix
         Keyboard_release(key3);
         Keyboard_release(key3f);
      }    
    }
  }
  delay(50);
}

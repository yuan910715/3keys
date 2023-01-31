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
bool key1media=false;
bool key2media=false;
bool key3media=false;

//keymap[0] start label
//keymap[1] key1
//keymap[2] key2
//keymap[3] key3
//keymap[4] key1media
//keymap[5] key2media
//keymap[6] key3media
//keymap[7] end label
char keymap[8]={0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff};
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
     key1media=eep3;
  }
  if(eep4!=0xff){
     key2media=eep4;
  }
  if(eep5!=0xff){
     key3media=eep5;
  }
}

void loop() {
	//read serial msg 和改键程序通讯
  while(USBSerial_available()) {
    char serialChar = USBSerial_read();
    if ((serialChar != '\n') && (serialChar != '\r') ) {
      if(label>7){
        label=0;
        }
      if(label==0 && serialChar!=0x03){
        continue;
      }
      keymap[label]=serialChar;
      label++;
    } 
  }
  if(label==8){
      label=0;
      if(keymap[0]==0x03 && keymap[7]=='@'){
        USBSerial_print_s("1");
        USBSerial_flush();
        eeprom_write_byte(0, keymap[1]);
        eeprom_write_byte(1, keymap[2]);
        eeprom_write_byte(2, keymap[3]);
        eeprom_write_byte(3, keymap[4]);
        eeprom_write_byte(4, keymap[5]);
        eeprom_write_byte(5, keymap[6]);
        key1=keymap[1];
        key2=keymap[2];
        key3=keymap[3];
        key1media=(bool)keymap[4];
        key2media=(bool)keymap[5];
        key3media=(bool)keymap[6];
      }else if(keymap[0]==0x03 && keymap[7]=='#'){
        USBSerial_println(key1,HEX);
        USBSerial_println(key2,HEX);
        USBSerial_println(key3,HEX);
        USBSerial_println((byte)key1media,HEX);
        USBSerial_println((byte)key2media,HEX);
        USBSerial_println((byte)key3media,HEX);
        USBSerial_flush();
      }
  }
  bool button1Press = !digitalRead(BUTTON1_PIN);
  if (button1PressPrev != button1Press) {
    button1PressPrev = button1Press;
    if (button1Press) {
      if(key1media){
        Consumer_press(key1);
      }else{
        Keyboard_press(key1);
      }
          
    }
    else {
      if(key1media){
         Consumer_release(key1);
      }else{
      Keyboard_release(key1);
      }
    }
  }
  bool button2Press = !digitalRead(BUTTON2_PIN);
  if (button2PressPrev != button2Press) 
  {
    button2PressPrev = button2Press;
    if (button2Press) {
        if(key2media){
        Consumer_press(key2);
      }else{
        Keyboard_press(key2);
      }
    } 
    else {
      if(key2media){
         Consumer_release(key2);
      }else{
      Keyboard_release(key2);
      }
    }
  }

  bool button3Press = !digitalRead(BUTTON3_PIN);
  if (button3PressPrev != button3Press) 
  {
    button3PressPrev = button3Press;
    if (button3Press) {
       if(key3media){
        Consumer_press(key3);
      }else{
        Keyboard_press(key3);
      }
    } 
    else {
     if(key3media){
         Consumer_release(key3);
      }else{
      Keyboard_release(key3);
      }
    }
  }
  delay(50);
}

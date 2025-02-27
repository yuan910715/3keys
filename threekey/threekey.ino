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

bool three_lock = false;

char key1=0xff;
char key2='a';
char key3=0xff;

char keyall=0xff;

// is mediakey?
byte key1func=0x02;
byte key2func=0x00;
byte key3func=0x02;
byte keyallfunc=0x02;

char key1f=0xff;
char key2f=0xff;
char key3f=0xff;
char keyallf=0xff;

char key1mix[5]={0x80,'c',0xff,0xff,0xff};
char key2mix[5]={0xff,0xff,0xff,0xff,0xff};
char key3mix[5]={0x80,'v',0xff,0xff,0xff};
char keyallmix[5]={0x87,'l',0xff,0xff,0xff};

__xdata char custStr1[30]={0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff};
__xdata char custStr2[30]={0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff};
__xdata char custStr3[30]={0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff};

byte keyallmode=0x01;

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
//keymap[100]-[104] key1mix[5]
//keymap[105]-[109] key2mix[5]
//keymap[110]-[114] key3mix[5]
//keymap[115] keyall
//keymap[116] keyallfunc
//keymap[117] keyall f
//keymap[118]-[147] custom strall
//keymap[148]-[152] keyallmix[5]
//keymap[153] keyallmode
//keymap[154] end label
__xdata char keymap[155]={0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,
                  0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,
                  0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,
                  0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,
                  0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,
                  0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,
                  0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,
                  0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,
                  0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,
                  0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,
                  0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,
                  0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,
                  0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,
                  0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,
                  0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,
                  0xff,0xff,0xff,0xff,0xff
                  };
int label=0;
void setup() {
  randomSeed(analogRead(0));
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
    uint8_t temp = eeprom_read_byte(9+i);
    if(temp!=0xff){
       custStr1[i] = temp;
    }
    temp = eeprom_read_byte(39+i);
    if(temp!=0xff){
       custStr2[i] = temp;
    }
    temp = eeprom_read_byte(69+i);
    if(temp!=0xff){
       custStr3[i] = temp;
    }
//     temp = eeprom_read_byte(117+i);
//     if(temp!=0xff){
//       custStrAll[i] = temp;
//    }
  }
  for(int i=0;i<5;i++){
    uint8_t temp = eeprom_read_byte(99+i);
    if(temp!=0xff){
      key1mix[i] = temp;
    }
    temp = eeprom_read_byte(104+i);
     if(temp!=0xff){
      key2mix[i] = temp;
    }
    temp = eeprom_read_byte(109+i);
     if(temp!=0xff){
      key3mix[i] = temp;
    }
    temp = eeprom_read_byte(117+i);
     if(temp!=0xff){
      keyallmix[i] = temp;
    }
  }
  uint8_t eep114 = eeprom_read_byte(114);
  if(eep114!=0xff){
     keyall=eep114;
  }
  uint8_t eep115 = eeprom_read_byte(115);
  if(eep115!=0xff){
     keyallfunc=eep115;
  }
  uint8_t eep116 = eeprom_read_byte(116);
  if(eep116!=0xff){
     keyallf=eep116;
  }
  uint8_t eep122 = eeprom_read_byte(122);
  if(eep122!=0xff){
     keyallmode=eep122;
  }
}

void loop() {
	//read serial msg
  while(USBSerial_available()) {
    char serialChar = USBSerial_read();
    if ((serialChar != '\n') && (serialChar != '\r') ) {
      if(label>154){
        label=0;
        }
      if(label==0 && serialChar!=0x02){
        continue;
      }
      if(label==1 && serialChar==0x03){
        USBSerial_println("v7");
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
         for(int i=0;i<5;i++){
            USBSerial_println(key1mix[i],HEX);
         }
         for(int i=0;i<5;i++){
            USBSerial_println(key2mix[i],HEX);
         }
         for(int i=0;i<5;i++){
            USBSerial_println(key3mix[i],HEX);
         }
        USBSerial_println(keyall,HEX);
        USBSerial_println(keyallfunc,HEX);
        USBSerial_println(keyallf,HEX);
//        for(int i=0;i<30;i++){
//            if(custStrAll[i]==0xff){
//              break;
//              }
//             USBSerial_print_c(custStrAll[i]);  
//         }
        USBSerial_println();
        for(int i=0;i<5;i++){
            USBSerial_println(keyallmix[i],HEX);
         }
        USBSerial_println(keyallmode,HEX);
        USBSerial_flush();
      	 label=0;
      	 continue;
      }
      keymap[label]=serialChar;
      label++;
    } 
  }
  if(label==155){
      label=0;
      if(keymap[0]==0x02 && keymap[154]==0x03){
        USBSerial_print_s("1");
        USBSerial_flush();
//        for(int i=0;i<153;i++){
//        eeprom_write_byte(i, keymap[i+1]);
//        }
          for(int i=0;i<117;i++){
             eeprom_write_byte(i, keymap[i+1]);
           }
            eeprom_write_byte(117, keymap[148]);
            eeprom_write_byte(118, keymap[149]);
            eeprom_write_byte(119, keymap[150]);
            eeprom_write_byte(120, keymap[151]);
            eeprom_write_byte(121, keymap[152]);
            eeprom_write_byte(122, keymap[153]);
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
//        custStrAll[i]=keymap[118+i];
        }
        for(int i=0;i<5;i++){
           key1mix[i] = keymap[100+i];
           key2mix[i] = keymap[105+i];
           key3mix[i] = keymap[110+i];
           keyallmix[i] = keymap[148+i];
        }
        keyall=keymap[115];
        keyallfunc=keymap[116];
        keyallf=keymap[117];
        keyallmode=keymap[153];
      }
  }
  bool button1Press = !digitalRead(BUTTON1_PIN);
  bool button2Press = !digitalRead(BUTTON2_PIN);
  bool button3Press = !digitalRead(BUTTON3_PIN);
  
  if(keyallmode==0x01){
  if(!three_lock){
     if(button1PressPrev != button1Press && button2PressPrev != button2Press && button3PressPrev != button3Press){
      if(button1Press && button2Press && button3Press){
         three_lock = true;
         button1PressPrev = button1Press;
         button2PressPrev = button2Press;
         button3PressPrev = button3Press;
             if(keyallfunc==0x00){  //normal
              Keyboard_press(keyall);
            }
            else if(keyallfunc==0x01){ //media
             if(keyallf!=0xff){
                Consumer_press((keyallf<<8)+keyall);
              }else{
              Consumer_press(keyall);
              }
            }
            else if(keyallfunc==0x02){ //mix
              for(int i=0;i<5;i++){
                if(keyallmix[i]!=0xff){
                  Keyboard_press(keyallmix[i]);
                  }
              }
            }
            else if(keyallfunc==0x03){ //string
//               for(int i=0;i<30;i++){
//                  if(custStrAll[i]==0xff){
//                    break;}
//                   Keyboard_write(custStrAll[i]);
//               }
            }
            else if(keyallfunc==0x04){ //mouse click
           if(keyallf==0x00){ //single
             Mouse_click(keyall);
           }else if(keyallf==0x01){ //hold
             while(true){
              Mouse_click(keyall);
               if(digitalRead(BUTTON1_PIN) || digitalRead(BUTTON2_PIN) || digitalRead(BUTTON3_PIN)){
                 three_lock=false;
                 break;
                 }
              } 
           }else if(keyallf==0x02){ //press
              Mouse_press(keyall);
           }
           
        }else if(keyallfunc==0x05){ //mouse move random
         //mouse move random hold
            while(true){
             Mouse_move(random(20)-10,random(20)-10);
             delay(100);
               if(digitalRead(BUTTON1_PIN) || digitalRead(BUTTON2_PIN) || digitalRead(BUTTON3_PIN)){
                   while(true){
                      Mouse_move(random(20)-10,random(20)-10);
                      delay(100);
                       if(!digitalRead(BUTTON1_PIN) && !digitalRead(BUTTON2_PIN) && !digitalRead(BUTTON3_PIN)){                    
                          break;
                      }
                    }                
                 three_lock=false;
                 break;
                 }             
            } 
         
      }else if(keyallfunc==0x06){ //mouse move
            if(keyallf==0x01){ //UP
              Mouse_move(0,(keyall-31)*-1);
            }else if(keyallf==0x02){ //DOWN
               Mouse_move(0,(keyall-31));
            }else if(keyallf==0x03){ //LEFT
               Mouse_move((keyall-31)*-1,0);
            }else if(keyallf==0x04){  //RIGHT
               Mouse_move((keyall-31),0);
            }
         } else if(keyallfunc==0x07){ //mouse move hold
            while(true){
            if(keyallf==0x01){ //UP
              Mouse_move(0,(keyall-31)*-1);
            }else if(keyallf==0x02){ //DOWN
               Mouse_move(0,(keyall-31));
            }else if(keyallf==0x03){ //LEFT
               Mouse_move((keyall-31)*-1,0);
            }else if(keyallf==0x04){  //RIGHT
               Mouse_move((keyall-31),0);
            }
            delay(100);
               if(digitalRead(BUTTON1_PIN) || digitalRead(BUTTON2_PIN) || digitalRead(BUTTON3_PIN)){
                 three_lock=false;
                 break;
                 }
        }
       }else if(keyallfunc==0x08){ //mouse scrool
             if(keyallf==0x01){ //UP
              Mouse_scroll(keyall);
            }else if(keyallf==0x02){ //DOWN
              Mouse_scroll(keyall*-1);            
            }
       }else if(keyallfunc==0x09){ //mouse scrool hold
            while(true){
             if(keyallf==0x01){ //UP
              Mouse_scroll(keyall);
            }else if(keyallf==0x02){ //DOWN
              Mouse_scroll(keyall*-1);
            }
            delay(100);
               if(digitalRead(BUTTON1_PIN) || digitalRead(BUTTON2_PIN) || digitalRead(BUTTON3_PIN)){
                 three_lock=false;
                 break;
                 }
            }
       }  
      
           
      }
    }
  }else{
    if(!button1Press || !button2Press || !button3Press){
      button1PressPrev = button1Press;
      button2PressPrev = button2Press;
      button3PressPrev = button3Press;
        if(keyallfunc==0x00){  //normal
          Keyboard_release(keyall);
        }
        else if(keyallfunc==0x01){ //media
          if(keyallf!=0xff){
            Consumer_release((keyallf<<8)+keyall);
          }
          Consumer_release(keyall);
        }
        else if(keyallfunc==0x02){ //mix
          for(int i=4;i>=0;i--){
            if(keyallmix[i]!=0xff){
              Keyboard_release(keyallmix[i]);
              }
          }
        }
        else if(keyallfunc==0x04&&keyallf==0x02){ //mouse     
          Mouse_release(keyall);
        }        
      three_lock = false;
      }
    }
  }else{
    three_lock=false;
  }
  if(!three_lock){
  if (button1PressPrev != button1Press) {
    button1PressPrev = button1Press;
    if (button1Press) {
      	
         if(key1func==0x00){  //normal
            Keyboard_press(key1);
      }
      else if(key1func==0x01){ //media
        if(key1f!=0xff){
          Consumer_press((key1f<<8)+key1);
        }
        Consumer_press(key1);
      }
      else if(key1func==0x02){ //mix
        for(int i=0;i<5;i++){
          if(key1mix[i]!=0xff){
            Keyboard_press(key1mix[i]);
            }
        }
      }
      else if(key1func==0x03){ //string
         for(int i=0;i<30;i++){
            if(custStr1[i]==0xff){
              break;}
             Keyboard_write(custStr1[i]);
         }
         //enter B0
         if(key1f==0x01){
          Keyboard_write(0xB0);
          }
      }
      else if(key1func==0x04){ //mouse click
           if(key1f==0x00){ //single
             Mouse_click(key1);
           }else if(key1f==0x01){ //hold
             while(true){
              Mouse_click(key1);
              if(digitalRead(BUTTON1_PIN)){
                 break;}
              } 
           }else if(key1f==0x02){ //press
              Mouse_press(key1);
           }
      }
      else if(key1func==0x05){ //mouse move random
         while(true){
           Mouse_move(random(20)-10,random(20)-10);
           delay(100);
             if(digitalRead(BUTTON1_PIN)){
                     while(true){
                      Mouse_move(random(20)-10,random(20)-10);
                      delay(100);
                      if(!digitalRead(BUTTON1_PIN)){
                        break;
                      }
                    }               
              break;
             }
           }
      }
      else if(key1func==0x06){ //mouse move
            if(key1f==0x01){ //UP
              Mouse_move(0,(key1-31)*-1);
            }else if(key1f==0x02){ //DOWN
               Mouse_move(0,(key1-31));
            }else if(key1f==0x03){ //LEFT
               Mouse_move((key1-31)*-1,0);
            }else if(key1f==0x04){  //RIGHT
               Mouse_move((key1-31),0);
            }
         }  
       else if(key1func==0x07){ //mouse move hold
            while(true){
            if(key1f==0x01){ //UP
              Mouse_move(0,(key1-31)*-1);
            }else if(key1f==0x02){ //DOWN
               Mouse_move(0,(key1-31));
            }else if(key1f==0x03){ //LEFT
               Mouse_move((key1-31)*-1,0);
            }else if(key1f==0x04){  //RIGHT
               Mouse_move((key1-31),0);
            }
            delay(100);
           if(digitalRead(BUTTON1_PIN)){
            break;
           }
        }
       }else if(key1func==0x08){ //mouse scrool
             if(key1f==0x01){ //UP
              Mouse_scroll(key1);
            }else if(key1f==0x02){ //DOWN
              Mouse_scroll(key1*-1);            
            }
       }else if(key1func==0x09){ //mouse scrool hold
            while(true){
             if(key1f==0x01){ //UP
              Mouse_scroll(key1);
            }else if(key1f==0x02){ //DOWN
              Mouse_scroll(key1*-1);
            }
            delay(100);
           if(digitalRead(BUTTON1_PIN)){
            break;
           }
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
        for(int i=4;i>=0;i--){
          if(key1mix[i]!=0xff){
            Keyboard_release(key1mix[i]);
            }
        }
      }
      else if(key1func==0x04&&key1f==0x02){ //mouse     
          Mouse_release(key1);
      }    
    }
  }

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
        for(int i=0;i<5;i++){
          if(key2mix[i]!=0xff){
            Keyboard_press(key2mix[i]);
            }
        }
      }
      else if(key2func==0x03){ //string
         for(int i=0;i<30;i++){
            if(custStr2[i]==0xff){
              break;}
             Keyboard_write(custStr2[i]);
         }
         //enter B0
         if(key2f==0x01){
          Keyboard_write(0xB0);
          }
      }
      else if(key2func==0x04){ //mouse click
           if(key2f==0x00){ //single
             Mouse_click(key2);
           }else if(key2f==0x01){ //hold
             while(true){
              Mouse_click(key2);
              if(digitalRead(BUTTON2_PIN)){
                 break;}
              } 
           }
           else if(key2f==0x02){ //press
              Mouse_press(key2);
           }
      }
      else if(key2func==0x05){ //mouse move random
         while(true){
           Mouse_move(random(20)-10,random(20)-10);
           delay(100);
             if(digitalRead(BUTTON2_PIN)){
                     while(true){
                      Mouse_move(random(20)-10,random(20)-10);
                      delay(100);
                      if(!digitalRead(BUTTON2_PIN)){
                        break;
                      }
                    }               
              break;
             }
           }
      }
      else if(key2func==0x06){ //mouse move
            if(key2f==0x01){ //UP
              Mouse_move(0,(key2-31)*-1);
            }else if(key2f==0x02){ //DOWN
               Mouse_move(0,(key2-31));
            }else if(key2f==0x03){ //LEFT
               Mouse_move((key2-31)*-1,0);
            }else if(key2f==0x04){  //RIGHT
               Mouse_move((key2-31),0);
            }
         }  
       else if(key2func==0x07){ //mouse move hold
            while(true){
            if(key2f==0x01){ //UP
              Mouse_move(0,(key2-31)*-1);
            }else if(key2f==0x02){ //DOWN
               Mouse_move(0,(key2-31));
            }else if(key2f==0x03){ //LEFT
               Mouse_move((key2-31)*-1,0);
            }else if(key2f==0x04){  //RIGHT
               Mouse_move((key2-31),0);
            }
            delay(100);
           if(digitalRead(BUTTON2_PIN)){
            break;
           }
        }
       }else if(key2func==0x08){ //mouse scrool
             if(key2f==0x01){ //UP
              Mouse_scroll(key2);
            }else if(key2f==0x02){ //DOWN
              Mouse_scroll(key2*-1);            
            }
       }else if(key2func==0x09){ //mouse scrool hold
            while(true){
             if(key2f==0x01){ //UP
              Mouse_scroll(key2);
            }else if(key2f==0x02){ //DOWN
              Mouse_scroll(key2*-1);
            }
            delay(100);
           if(digitalRead(BUTTON2_PIN)){
            break;
           }
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
         for(int i=4;i>=0;i--){
          if(key2mix[i]!=0xff){
            Keyboard_release(key2mix[i]);
            }
        }
      }
      else if(key2func==0x04&&key2f==0x02){ //mouse     
          Mouse_release(key2);
      }    
    }
  }


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
         for(int i=0;i<5;i++){
          if(key3mix[i]!=0xff){
            Keyboard_press(key3mix[i]);
            }
        }
      }
      else if(key3func==0x03){ //string
         for(int i=0;i<30;i++){
            if(custStr3[i]==0xff){
              break;}
             Keyboard_write(custStr3[i]);
         }
         //enter B0
         if(key3f==0x01){
          Keyboard_write(0xB0);
          }
      }
      else if(key3func==0x04){ //mouse click
           if(key3f==0x00){ //single
             Mouse_click(key3);
           }else if(key3f==0x01){ //hold
             while(true){
              Mouse_click(key3);
              if(digitalRead(BUTTON3_PIN)){
                 break;}
              } 
           }else if(key3f==0x02){ //press
              Mouse_press(key3);
           }
      }
      else if(key3func==0x05){ //mouse move random
         while(true){
           Mouse_move(random(20)-10,random(20)-10);
           delay(100);
             if(digitalRead(BUTTON3_PIN)){
                     while(true){
                      Mouse_move(random(20)-10,random(20)-10);
                      delay(100);
                      if(!digitalRead(BUTTON3_PIN)){
                        break;
                      }
                    }
              break;
             }
           }
      }
      else if(key3func==0x06){ //mouse move
            if(key3f==0x01){ //UP
              Mouse_move(0,(key3-31)*-1);
            }else if(key3f==0x02){ //DOWN
               Mouse_move(0,(key3-31));
            }else if(key3f==0x03){ //LEFT
               Mouse_move((key3-31)*-1,0);
            }else if(key3f==0x04){  //RIGHT
               Mouse_move((key3-31),0);
            }
         }  
       else if(key3func==0x07){ //mouse move hold
            while(true){
            if(key3f==0x01){ //UP
              Mouse_move(0,(key3-31)*-1);
            }else if(key3f==0x02){ //DOWN
               Mouse_move(0,(key3-31));
            }else if(key3f==0x03){ //LEFT
               Mouse_move((key3-31)*-1,0);
            }else if(key3f==0x04){  //RIGHT
               Mouse_move((key3-31),0);
            }
            delay(100);
           if(digitalRead(BUTTON3_PIN)){
            break;
           }
        }
       }else if(key3func==0x08){ //mouse scrool
             if(key3f==0x01){ //UP
              Mouse_scroll(key3);
            }else if(key3f==0x02){ //DOWN
              Mouse_scroll(key3*-1);            
            }
       }else if(key3func==0x09){ //mouse scrool hold
            while(true){
             if(key3f==0x01){ //UP
              Mouse_scroll(key3);
            }else if(key3f==0x02){ //DOWN
              Mouse_scroll(key3*-1);
            }
            delay(100);
           if(digitalRead(BUTTON3_PIN)){
            break;
           }
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
          for(int i=4;i>=0;i--){
          if(key3mix[i]!=0xff){
            Keyboard_release(key3mix[i]);
            }
        }
      }
      else if(key3func==0x04&&key3f==0x02){ //mouse     
          Mouse_release(key3);
      }
    }
  }
 }
  delay(50);
}

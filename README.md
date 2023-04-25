# 3keys
一个CH552制作的三键小键盘 CV工程师键盘

视频介绍https://www.bilibili.com/video/BV1M84y1n7r1

CH552芯片是一款兼容MCS51 指令集的增强型E8051内核单片机，其79%的指令是单字节单周期指令，平均指令速度比标准MCS51快8～15 倍。
CH552 支持最高24MHz 系统主频，内置16K 程序存储器ROM 和256 字节内部iRAM 以及1K 字节片内xRAM，xRAM 支持DMA直接内存存取。
CH552内置了ADC 模数转换、触摸按键电容检测、3 组定时器和信号捕捉及PWM、双异步串口、SPI、USB设备控制器和全速收发器、USB type-C等功能模块。

你可以直接打板制作，使用沁恒(CH552厂家)的WCHISPStudio工具烧录threekey.ino.hex即可。 3 key tools.exe为改键程序,windows下使用 需有.Net framework 4.0

可以使用Chrome浏览器直接访问https://topyuan.top/3key 进行改键。 3key-web目录为web改键源码 使用Vue + Element UI开发

![web](https://github.com/yuan910715/3keys/blob/main/pics/web.jpg)

ch552g目录下为 键盘程序源码。开发环境参考https://github.com/DeqingSun/ch55xduino 感谢DeqingSun的指导https://github.com/DeqingSun/ch55xduino/issues/112

WindowsFormsApp1目录为改键程序源码。使用visual studio .net framework 4.0开发

![0](https://github.com/yuan910715/3keys/blob/main/pics/0.jpg)

# 软件

特点
- 可自定义按键，有改键程序，可以是普通按键 功能按键 多媒体按键 组合键 输入字符串
- 每个键位的设置可以记忆在硬件内 换机器也可正常使用

## 2023.4.25 软件更新为threekey_v4.1.hex 修复了windows在刚连接键盘就立刻按键的情形 按键失效问题  感谢DeqingSun的指导https://github.com/DeqingSun/ch55xduino/issues/132

## 2023.3.27 软件更新为threekey_v4.hex 修改组合键逻辑 最多支持5键组合 新增三键同时按下逻辑 web改键已同步更新，windows改键程序已废弃，若在无外网环境无法访问web改键 可本地chrome浏览器打开3key-web里的index.html修改按键 程序也有所更新 以下代码解析非最新代码 请自行比对代码

代码解析： threekey.ino中 ,

全局变量如下：


三个按键的按下或释放状态：
```
bool button1PressPrev = false;
bool button2PressPrev = false;
bool button3PressPrev = false;
```
三个按键的键位设置, 默认为ctrl c v
```
//ctrl
char key1=0x80;
//c
char key2='c';
//v
char key3='v';
```

3个按键的功能种类 0为普通按键 1为多媒体按键 2为组合键 3为字符串
```
// is mediakey?
byte key1func=0x00;
byte key2func=0x00;
byte key3func=0x00;
```

3个按键为组合键时 功能键的键位值; 为多媒体键时 若为16位无符号整数 为高位值
```
char key1f=0xff;
char key2f=0xff;
char key3f=0xff;
```

3个按键的字符串值 最大30位
```
__xdata char custStr1[30]={0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff};
__xdata char custStr2[30]={0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff};
__xdata char custStr3[30]={0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff};
```

用于接收改键程序设置的报文
```
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
```

接收改键程序报文时使用
```
int label=0;
```

setup()方法内, 设置3个按键位内部上拉模式, 并从eeprom读取3个按键的键位信息

loop()方法内, 从串口读取报文 约定0x02为起始位标记 0x03为结束位标记， 报文长度101与keymap对应 , 若为0x02 0x03, 约定为查询报文 返回键位信息
```
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
```

设置报文接收完毕 改键位值 并写入eeprom
```
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
```

判断按键按下状态 判断key1fuc值决定是哪种按键模式  执行相应操作  唯一特殊的是多媒体键有8位整数和16位两种 如为16位 key1f会保存高8位值 需左移8位加上低8位的key1
```
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
```

改键程序：

![gaijian2](https://github.com/yuan910715/3keys/blob/main/pics/gaijian2.png)

改键程序代码不再详细解析, 原理为串口和键盘通讯 发送0x02 0x03查询键位值 , 发送
```
0x02 key1 key2 key3 key1func key2func key2func key1f key2f key3c key1string(30字节) key2string(30字节) key3string(30字节) 0x03
```
设置键位值

示例,将3个按键都改为模式3字符串：
```
02 63 76 78 03 03 03 80 81 82 41 42 43 ff ff ff ff ff ff ff ff ff ff ff ff ff ff ff ff ff ff ff ff ff ff ff ff ff ff ff 43 43 ff ff ff ff ff ff ff ff ff ff ff ff ff ff ff ff ff ff ff ff ff ff ff ff ff ff ff ff 41 41 ff ff ff ff ff ff ff ff ff ff ff ff ff ff ff ff ff ff ff ff ff ff ff ff ff ff ff ff 03
```

USBconstant.c内可更改连接到电脑后 识别的设备名称:
![3-key](https://github.com/yuan910715/3keys/blob/main/pics/3-key.png)

```
__code uint16_t Prod_Des[]={                                //Produce String Descriptor
    0x0316,
    '-','Y','u','a','n','-','3','k','e','y',
};
```

# 硬件

感谢phantomR ，我把小灯删了，Type C换成了0.58元的便宜版本。 硬件只需2个100nf电容(0603) 一个CH552G 一个typeC母座(优信0.58那个有柱16p) 一个20k欧电阻 一个下载开关(不焊也行我就没焊 CH552G首次自动进写入模式 后续要进就用镊子短接)

硬件开源地址
https://oshwhub.com/inramento/3jian-CVxiao-jian-pan


原理图：
![yuanlitu](https://github.com/yuan910715/3keys/blob/main/pics/yuanlitu.png)

2023.3.9更新 V3.0版本 新增了R2 R3 两个5.1K电阻 用于C TO C线 如果不焊接这两个电阻 将只能使用 A TO C线   加大了TYPE C焊盘 降低焊接难度
v3_1：
![v3_1](https://github.com/yuan910715/3keys/blob/main/pics/V3_1.jpg)

v3_2：
![v3_2](https://github.com/yuan910715/3keys/blob/main/pics/V3_2.jpg)

v3_3：
![v3_3](https://github.com/yuan910715/3keys/blob/main/pics/V3_3.png)

pcb1：
![pcb1](https://github.com/yuan910715/3keys/blob/main/pics/pcb1.png)

pcb2：
![pcb2](https://github.com/yuan910715/3keys/blob/main/pics/pcb2.png)

pcb3：
![pcb3](https://github.com/yuan910715/3keys/blob/main/pics/pcb3.png)

pcb4：
![pcb4](https://github.com/yuan910715/3keys/blob/main/pics/pcb4.png)

pcb5：
![pcb5](https://github.com/yuan910715/3keys/blob/main/pics/pcb5.png)

黑色版本 有小灯 因为效果不好 板子上已经把小灯删了
![1](https://github.com/yuan910715/3keys/blob/main/pics/1.jpg)

![2](https://github.com/yuan910715/3keys/blob/main/pics/2.jpg)

![3](https://github.com/yuan910715/3keys/blob/main/pics/3.jpg)

![4](https://github.com/yuan910715/3keys/blob/main/pics/4.jpg)

![5_1](https://github.com/yuan910715/3keys/blob/main/pics/5_1.jpg)

![6](https://github.com/yuan910715/3keys/blob/main/pics/6.jpg)

![0](https://github.com/yuan910715/3keys/blob/main/pics/0.jpg)



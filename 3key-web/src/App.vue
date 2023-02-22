<template>
  <div id="app">
    <div>
       <el-row>      
          <img src="./assets/key1.png">
      </el-row>  
      <el-row>      
        <el-button :type="conButtonType" @click="conClick()">{{ conButton }}</el-button>       
      </el-row>
      <el-row>
        <label style="margin-right: 20px;">key1:</label>
        <el-radio-group v-model="key1radio" @change="key1fucChange()">
          <el-radio :label="1">普通键</el-radio>
          <el-radio :label="2">功能键</el-radio>
          <el-radio :label="3">多媒体键</el-radio>
          <el-radio :label="4">组合键</el-radio>
          <el-radio :label="5">字符串</el-radio>
        </el-radio-group>
      </el-row>
      <el-row>
        <el-select v-model="key1funcselectvalue" placeholder="请选择" v-if="key1funcselectshow">
          <el-option
            v-for="item in key1funcoptions"
            :key="item.value"
            :label="item.label"
            :value="item.value">
          </el-option>
        </el-select>
        <el-select v-model="key1mediaselectvalue" placeholder="请选择" v-if="key1mediaselectshow" style="width:400px">
          <el-option
            v-for="item in key1mediaoptions"
            :key="item.value"
            :label="item.label"
            :value="item.value">
          </el-option>
        </el-select>
        <el-input style="width:70px" type="text" v-model="key1text1" maxlength="1" show-word-limit v-if="key1input1show"></el-input>       
        <el-input style="width:350px" type="text" v-model="key1text2" maxlength="30" show-word-limit clearable v-if="key1input2show"></el-input>      
      </el-row>
      <el-row>
        <label style="margin-right: 20px;">key2:</label>
        <el-radio-group v-model="key2radio" @change="key2fucChange()">
          <el-radio :label="1">普通键</el-radio>
          <el-radio :label="2">功能键</el-radio>
          <el-radio :label="3">多媒体键</el-radio>
          <el-radio :label="4">组合键</el-radio>
          <el-radio :label="5">字符串</el-radio>
        </el-radio-group>
      </el-row>
      <el-row>
        <el-select v-model="key2funcselectvalue" placeholder="请选择" v-if="key2funcselectshow">
          <el-option
            v-for="item in key2funcoptions"
            :key="item.value"
            :label="item.label"
            :value="item.value">
          </el-option>
        </el-select>
        <el-select v-model="key2mediaselectvalue" placeholder="请选择" v-if="key2mediaselectshow" style="width:400px">
          <el-option
            v-for="item in key2mediaoptions"
            :key="item.value"
            :label="item.label"
            :value="item.value">
          </el-option>
        </el-select>
        <el-input style="width:70px" type="text" v-model="key2text1" maxlength="1" show-word-limit v-if="key2input1show"></el-input>       
        <el-input style="width:350px" type="text" v-model="key2text2" maxlength="30" show-word-limit clearable v-if="key2input2show"></el-input>      
      </el-row>
      <el-row>
        <label style="margin-right: 20px;">key3:</label>
        <el-radio-group v-model="key3radio" @change="key3fucChange()">
          <el-radio :label="1">普通键</el-radio>
          <el-radio :label="2">功能键</el-radio>
          <el-radio :label="3">多媒体键</el-radio>
          <el-radio :label="4">组合键</el-radio>
          <el-radio :label="5">字符串</el-radio>
        </el-radio-group>
      </el-row>
      <el-row>
        <el-select v-model="key3funcselectvalue" placeholder="请选择" v-if="key3funcselectshow">
          <el-option
            v-for="item in key3funcoptions"
            :key="item.value"
            :label="item.label"
            :value="item.value">
          </el-option>
        </el-select>
        <el-select v-model="key3mediaselectvalue" placeholder="请选择" v-if="key3mediaselectshow" style="width:400px">
          <el-option
            v-for="item in key3mediaoptions"
            :key="item.value"
            :label="item.label"
            :value="item.value">
          </el-option>
        </el-select>
        <el-input style="width:70px" type="text" v-model="key3text1" maxlength="1" show-word-limit v-if="key3input1show"></el-input>       
        <el-input style="width:350px" type="text" v-model="key3text2" maxlength="30" show-word-limit clearable v-if="key3input2show"></el-input>      
      </el-row>
      <el-row>
        <el-button type="primary" @click="sendClick()" :disabled="sendLabel">提交</el-button>   
      </el-row>
      <el-row>
        <img src="./assets/key2.png">
      </el-row> 
    </div>
  </div>
</template>

<script>
export default {
  methods: {
    key1fucChange(){
      switch(this.key1radio){
        case 1://normal
          this.key1input1show = true;
          this.key1funcselectshow = false;
          this.key1mediaselectshow = false;
          this.key1input2show = false;
          break;
        case 2://function
          this.key1input1show = false;
          this.key1funcselectshow = true;
          this.key1mediaselectshow = false;
          this.key1input2show = false;
          this.key1funcoptions.splice(0,this.key1funcoptions.length);
          for(var i=0;i<this.functionkeys.length;i++){
            this.key1funcoptions.push({value:this.functionkeyvalues[i],label:this.functionkeys[i]});
          }
          break;
        case 3://media
          this.key1input1show = false;
          this.key1funcselectshow = false;
          this.key1mediaselectshow = true;
          this.key1input2show = false;
          this.key1mediaoptions.splice(0,this.key1mediaoptions.length);
          for(var i=0;i<this.mediakeys.length;i++){
            this.key1mediaoptions.push({value:this.mediakeyvalues[i],label:this.mediakeys[i]});
          }
          break;
        case 4://mix
          this.key1input1show = true;
          this.key1funcselectshow = true;
          this.key1mediaselectshow = false;
          this.key1input2show = false;
          this.key1funcoptions.splice(0,this.key1funcoptions.length);
          for(var i=0;i<this.functionkeys.length;i++){
            this.key1funcoptions.push({value:this.functionkeyvalues[i],label:this.functionkeys[i]});
          }
          break;
        case 5://string
          this.key1input1show = false;
          this.key1funcselectshow = false;
          this.key1mediaselectshow = false;
          this.key1input2show = true;
          break;
        default://none
          this.key1input1show = false;
          this.key1funcselectshow = false;
          this.key1mediaselectshow = false;
          this.key1input2show = false;
          break;        
      }
 
    },
    key2fucChange(){
      switch(this.key2radio){
        case 1://normal
          this.key2input1show = true;
          this.key2funcselectshow = false;
          this.key2mediaselectshow = false;
          this.key2input2show = false;
          break;
        case 2://function
          this.key2input1show = false;
          this.key2funcselectshow = true;
          this.key2mediaselectshow = false;
          this.key2input2show = false;
          this.key2funcoptions.splice(0,this.key2funcoptions.length);
          for(var i=0;i<this.functionkeys.length;i++){
            this.key2funcoptions.push({value:this.functionkeyvalues[i],label:this.functionkeys[i]});
          }
          break;
        case 3://media
          this.key2input1show = false;
          this.key2funcselectshow = false;
          this.key2mediaselectshow = true;
          this.key2input2show = false;
          this.key2mediaoptions.splice(0,this.key2mediaoptions.length);
          for(var i=0;i<this.mediakeys.length;i++){
            this.key2mediaoptions.push({value:this.mediakeyvalues[i],label:this.mediakeys[i]});
          }
          break;
        case 4://mix
          this.key2input1show = true;
          this.key2funcselectshow = true;
          this.key2mediaselectshow = false;
          this.key2input2show = false;
          this.key2funcoptions.splice(0,this.key2funcoptions.length);
          for(var i=0;i<this.functionkeys.length;i++){
            this.key2funcoptions.push({value:this.functionkeyvalues[i],label:this.functionkeys[i]});
          }
          break;
        case 5://string
          this.key2input1show = false;
          this.key2funcselectshow = false;
          this.key2mediaselectshow = false;
          this.key2input2show = true;
          break;
        default://none
          this.key2input1show = false;
          this.key2funcselectshow = false;
          this.key2mediaselectshow = false;
          this.key2input2show = false;
          break;        
      }
 
    },
    key3fucChange(){
      switch(this.key3radio){
        case 1://normal
          this.key3input1show = true;
          this.key3funcselectshow = false;
          this.key3mediaselectshow = false;
          this.key3input2show = false;
          break;
        case 2://function
          this.key3input1show = false;
          this.key3funcselectshow = true;
          this.key3mediaselectshow = false;
          this.key3input2show = false;
          this.key3funcoptions.splice(0,this.key3funcoptions.length);
          for(var i=0;i<this.functionkeys.length;i++){
            this.key3funcoptions.push({value:this.functionkeyvalues[i],label:this.functionkeys[i]});
          }
          break;
        case 3://media
          this.key3input1show = false;
          this.key3funcselectshow = false;
          this.key3mediaselectshow = true;
          this.key3input2show = false;
          this.key3mediaoptions.splice(0,this.key3mediaoptions.length);
          for(var i=0;i<this.mediakeys.length;i++){
            this.key3mediaoptions.push({value:this.mediakeyvalues[i],label:this.mediakeys[i]});
          }
          break;
        case 4://mix
          this.key3input1show = true;
          this.key3funcselectshow = true;
          this.key3mediaselectshow = false;
          this.key3input2show = false;
          this.key3funcoptions.splice(0,this.key3funcoptions.length);
          for(var i=0;i<this.functionkeys.length;i++){
            this.key3funcoptions.push({value:this.functionkeyvalues[i],label:this.functionkeys[i]});
          }
          break;
        case 5://string
          this.key3input1show = false;
          this.key3funcselectshow = false;
          this.key3mediaselectshow = false;
          this.key3input2show = true;
          break;
        default://none
          this.key3input1show = false;
          this.key3funcselectshow = false;
          this.key3mediaselectshow = false;
          this.key3input2show = false;
          break;        
      }
 
    },
    conClick(){
      if(this.sendLabel==false){
        this.sendLabel=true;
        this.conButton="连接3 Key键盘";
        this.conButtonType="primary";
        this.key1radio=0;
        this.key1funcselectvalue="";
        this.key1mediaselectvalue="";
        this.key1fucChange();
        this.key2radio=0;
        this.key2funcselectvalue="";
        this.key2mediaselectvalue="";
        this.key2fucChange();
        this.key3radio=0;
        this.key3funcselectvalue="";
        this.key3mediaselectvalue="";
        this.key3fucChange();
        this.disconnectSerial();
      }
      else if (navigator.serial) {
		     this.connectSerial();   
		   } else {
		     alert('请使用新版本Chrome或Edge浏览器,https方式访问此页面!');
		   }
    },
    async sendCommit(){
      const data = new Uint8Array([0x02, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                    0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                    0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                    0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                    0x03 ]);
      switch(this.key1radio){
          case 1://normal
              if(this.key1text1.length!=1){
                this.$message.error('key1输入错误');
                return;
              }
              let key1tmp=this.key1text1.charCodeAt(0);
                if(key1tmp<0||key1tmp>255){
                  this.$message.error('key1输入错误');
                  return;
                }                
                data[1]=key1tmp;
                data[4]=0x00;                             
              break;
          case 2://function
              if(this.key1funcselectvalue==""){
                this.$message.error('key1选择错误');
                return;
              }          
                data[1]=this.key1funcselectvalue;
                data[4]=0x00;     
              break;
          case 3://media
              if(this.key1mediaselectvalue==""){
                this.$message.error('key1选择错误');
                return;
              }          
                data[1]=this.key1mediaselectvalue & 0xff;
                if(((this.key1mediaselectvalue>>8) & 0xff) !=0){
                  data[7] = (this.key1mediaselectvalue>>8) & 0xff;
                }
                data[4]=0x01;     
              break;
          case 4://mix
              if(this.key1funcselectvalue==""){
                this.$message.error('key1选择错误');
                return;
              }
              if(this.key1text1.length!=1){
                this.$message.error('key1输入错误');
                return;
              }
              let key1mixtmp=this.key1text1.charCodeAt(0);
                if(key1mixtmp<0||key1mixtmp>255){
                  this.$message.error('key1输入错误');
                  return;
                }                
                data[1]=key1mixtmp;
                data[4]=0x02;        
                data[7]=this.key1funcselectvalue;
            break; 
          case 5://string
            if(this.key1text2.length==0){
                this.$message.error('key1字符串不能为空');
                return;            
            }
            for(var i=0;i<this.key1text2.length;i++){
              if(this.key1text2.charCodeAt(i)<0||this.key1text2.charCodeAt(i)>255){
                this.$message.error('key1字符串输入错误，无此键位');
                return;            
              }
              data[10+i]=this.key1text2.charCodeAt(i);
            }
            data[4]=0x03;
            break;
          default://none
          this.$message.error('key1设置错误');
          return;
      }
      switch(this.key2radio){
          case 1://normal
              if(this.key2text1.length!=1){
                this.$message.error('key2输入错误');
                return;
              }
              let key2tmp=this.key2text1.charCodeAt(0);
                if(key2tmp<0||key2tmp>255){
                  this.$message.error('key2输入错误');
                  return;
                }                
                data[2]=key2tmp;
                data[5]=0x00;                             
              break;
          case 2://function
              if(this.key2funcselectvalue==""){
                this.$message.error('key2选择错误');
                return;
              }          
                data[2]=this.key2funcselectvalue;
                data[5]=0x00;     
              break;
          case 3://media
              if(this.key2mediaselectvalue==""){
                this.$message.error('key2选择错误');
                return;
              }          
                data[2]=this.key2mediaselectvalue & 0xff;
                if(((this.key2mediaselectvalue>>8) & 0xff) !=0){
                  data[8] = (this.key2mediaselectvalue>>8) & 0xff;
                }
                data[5]=0x01;     
              break;
          case 4://mix
              if(this.key2funcselectvalue==""){
                this.$message.error('key2选择错误');
                return;
              }
              if(this.key2text1.length!=1){
                this.$message.error('key2输入错误');
                return;
              }
              let key2mixtmp=this.key2text1.charCodeAt(0);
                if(key2mixtmp<0||key2mixtmp>255){
                  this.$message.error('key2输入错误');
                  return;
                }                
                data[2]=key2mixtmp;
                data[5]=0x02;        
                data[8]=this.key2funcselectvalue;
            break; 
          case 5://string
            if(this.key2text2.length==0){
                this.$message.error('key2字符串不能为空');
                return;            
            }
            for(var i=0;i<this.key2text2.length;i++){
              if(this.key2text2.charCodeAt(i)<0||this.key2text2.charCodeAt(i)>255){
                this.$message.error('key2字符串输入错误，无此键位');
                return;            
              }
              data[40+i]=this.key2text2.charCodeAt(i);
            }
            data[5]=0x03;
            break;
          default://none
          this.$message.error('key2设置错误');
          return;
      }
      switch(this.key3radio){
          case 1://normal
              if(this.key3text1.length!=1){
                this.$message.error('key3输入错误');
                return;
              }
              let key3tmp=this.key3text1.charCodeAt(0);
                if(key3tmp<0||key3tmp>255){
                  this.$message.error('key3输入错误');
                  return;
                }                
                data[3]=key3tmp;
                data[6]=0x00;                             
              break;
          case 2://function
              if(this.key3funcselectvalue==""){
                this.$message.error('key3选择错误');
                return;
              }          
                data[3]=this.key3funcselectvalue;
                data[6]=0x00;     
              break;
          case 3://media
              if(this.key3mediaselectvalue==""){
                this.$message.error('key3选择错误');
                return;
              }          
                data[3]=this.key3mediaselectvalue & 0xff;
                if(((this.key3mediaselectvalue>>8) & 0xff) !=0){
                  data[9] = (this.key3mediaselectvalue>>8) & 0xff;
                }
                data[6]=0x01;     
              break;
          case 4://mix
              if(this.key3funcselectvalue==""){
                this.$message.error('key3选择错误');
                return;
              }
              if(this.key3text1.length!=1){
                this.$message.error('key3输入错误');
                return;
              }
              let key3mixtmp=this.key3text1.charCodeAt(0);
                if(key3mixtmp<0||key3mixtmp>255){
                  this.$message.error('key3输入错误');
                  return;
                }                
                data[3]=key3mixtmp;
                data[6]=0x02;        
                data[9]=this.key3funcselectvalue;
            break; 
          case 5://string
            if(this.key3text2.length==0){
                this.$message.error('key3字符串不能为空');
                return;            
            }
            for(var i=0;i<this.key3text2.length;i++){
              if(this.key3text2.charCodeAt(i)<0||this.key3text2.charCodeAt(i)>255){
                this.$message.error('key3字符串输入错误，无此键位');
                return;            
              }
              data[70+i]=this.key3text2.charCodeAt(i);
            }
            data[6]=0x03;
            break;
          default://none
          this.$message.error('key3设置错误');
          return;
      }
      // console.log(data);
		  await this.writer.write(data);
    },
    async sendQuery(){
      const data = new Uint8Array([0x02,0x03]);
		  await this.writer.write(data);
    },
    sendClick(){
      this.sendCommit();
    },
    async connectSerial(){	
          const filters = [
          { usbVendorId: 0x1209, usbProductId: 0xC550 }
        ];	   		      
		    try {
		      this.port = await navigator.serial.requestPort({ filters });
		      await this.port.open({ baudRate: 115200 });		      
			    this.writer = this.port.writable.getWriter();
			    const decoder = new TextDecoderStream();		
		      const readableStreamClosed = this.port.readable.pipeTo(decoder.writable);
          
		      const inputStream = decoder.readable;
		      // this.reader = inputStream.getReader();
          this.reader = decoder.readable.pipeThrough(new TransformStream(new LineBreakTransformer())).getReader();
          // this.reader= this.port.readable.pipeThrough(new TransformStream(new LineBreakTransformer())).getReader();
		      this.sendLabel=false;
          this.conButton='断开连接';
          this.conButtonType='warning';
          // await this.port.setSignals({dataTerminalReady:true});
          await this.sendQuery();
          let label=0;
		      while (true) {
		        const { value, done } = await this.reader.read();
               if(label==0){
                if(value=="1"){
                  this.$message({
                    message:"设置成功!",
                    type:"success"
                  });
                  label=0;
                  continue;
                }
                this.key1=parseInt(value,16);
               }else if(label==1){
                this.key2=parseInt(value,16);
               }else if(label==2){
                this.key3=parseInt(value,16);
               }else if(label==3){
                this.key1func=parseInt(value,16);
               }else if(label==4){
                this.key2func=parseInt(value,16);
               }else if(label==5){
                this.key3func=parseInt(value,16);
               }else if(label==6){
                this.key1f=parseInt(value,16);
               }else if(label==7){
                this.key2f=parseInt(value,16);
               }else if(label==8){
                this.key3f=parseInt(value,16);
               }else if(label==9){
                this.key1text2=value;
               }else if(label==10){
                this.key2text2=value;
               }else if(label==11){
                this.key3text2=value;
               }

		        label++;
            if(label>=12){
              label=0;             
              switch(this.key1func){
                case 1://media
                  this.key1radio=3;
                  this.key1fucChange();
                  if(this.key1f==0xff){
                    for(var i=0;i<this.mediakeyvalues.length;i++){
                      if(this.mediakeyvalues[i]==this.key1){                     
                        this.key1mediaselectvalue=this.key1;
                      }
                  }
                  }else{
                    let tmp=Number(this.key1f<<8)+Number(this.key1);
                    for(var i=0;i<this.mediakeyvalues.length;i++){
                      if(this.mediakeyvalues[i]==tmp){                     
                        this.key1mediaselectvalue=tmp;
                      }
                  }                    
                  }
                  break;
                case 2://mix
                  this.key1radio=4;
                  this.key1fucChange();
                  for(var i=0;i<this.functionkeyvalues.length;i++){
                    if(this.functionkeyvalues[i]==this.key1f){
                      this.key1funcselectvalue=this.key1f;
                    }
                  }
                  this.key1text1=String.fromCharCode(this.key1);
                  break;
                case 3://string
                  this.key1radio=5;
                  this.key1fucChange();
                  break;
                default://normal
                  let key1label=false;
                  //function
                  for(var i=0;i<this.functionkeyvalues.length;i++){
                    if(this.functionkeyvalues[i]==this.key1){
                      this.key1radio=2;
                      this.key1fucChange();
                      this.key1funcselectvalue=this.key1;
                      key1label=true;
                    }
                  }
                  if(!key1label){//normal
                    this.key1radio=1;
                    this.key1fucChange();
                    this.key1text1=String.fromCharCode(this.key1);
                  }
                  break;
              }
              switch(this.key2func){
                case 1://media
                  this.key2radio=3;
                  this.key2fucChange();
                  if(this.key2f==0xff){
                    for(var i=0;i<this.mediakeyvalues.length;i++){
                      if(this.mediakeyvalues[i]==this.key2){                     
                        this.key2mediaselectvalue=this.key2;
                      }
                  }
                  }else{
                    let tmp=Number(this.key2f<<8)+Number(this.key2);
                    for(var i=0;i<this.mediakeyvalues.length;i++){
                      if(this.mediakeyvalues[i]==tmp){                     
                        this.key2mediaselectvalue=tmp;
                      }
                  }                    
                  }
                  break;
                case 2://mix
                  this.key2radio=4;
                  this.key2fucChange();
                  for(var i=0;i<this.functionkeyvalues.length;i++){
                    if(this.functionkeyvalues[i]==this.key2f){
                      this.key2funcselectvalue=this.key2f;
                    }
                  }
                  this.key2text1=String.fromCharCode(this.key2);
                  break;
                case 3://string
                  this.key2radio=5;
                  this.key2fucChange();
                  break;
                default://normal
                  let key2label=false;
                  //function
                  for(var i=0;i<this.functionkeyvalues.length;i++){
                    if(this.functionkeyvalues[i]==this.key2){
                      this.key2radio=2;
                      this.key2fucChange();
                      this.key2funcselectvalue=this.key2;
                      key2label=true;
                    }
                  }
                  if(!key2label){//normal
                    this.key2radio=1;
                    this.key2fucChange();
                    this.key2text1=String.fromCharCode(this.key2);
                  }
                  break;
              }
              switch(this.key3func){
                case 1://media
                  this.key3radio=3;
                  this.key3fucChange();
                  if(this.key3f==0xff){
                    for(var i=0;i<this.mediakeyvalues.length;i++){
                      if(this.mediakeyvalues[i]==this.key3){                     
                        this.key3mediaselectvalue=this.key3;
                      }
                  }
                  }else{
                    let tmp=Number(this.key3f<<8)+Number(this.key3);
                    for(var i=0;i<this.mediakeyvalues.length;i++){
                      if(this.mediakeyvalues[i]==tmp){                     
                        this.key3mediaselectvalue=tmp;
                      }
                  }                    
                  }
                  break;
                case 2://mix
                  this.key3radio=4;
                  this.key3fucChange();
                  for(var i=0;i<this.functionkeyvalues.length;i++){
                    if(this.functionkeyvalues[i]==this.key3f){
                      this.key3funcselectvalue=this.key3f;
                    }
                  }
                  this.key3text1=String.fromCharCode(this.key3);
                  break;
                case 3://string
                  this.key3radio=5;
                  this.key3fucChange();
                  break;
                default://normal
                  let key3label=false;
                  //function
                  for(var i=0;i<this.functionkeyvalues.length;i++){
                    if(this.functionkeyvalues[i]==this.key3){
                      this.key3radio=2;
                      this.key3fucChange();
                      this.key3funcselectvalue=this.key3;
                      key3label=true;
                    }
                  }
                  if(!key3label){//normal
                    this.key3radio=1;
                    this.key3fucChange();
                    this.key3text1=String.fromCharCode(this.key3);
                  }
                  break;
              }
            }
		        if (done) {
		          console.log('已断开'); 
              await readableStreamClosed.catch(()=>{});       
		          break;
		        }
		      }
		    } catch (error) {
          if(error.name=='NotFoundError'){
            return;
          }
          else if(error.name=='InvalidStateError'||error.name=='NetworkError'){
            this.$message.error('端口已占用,连接失败!');
          }
          else{
            this.$message.error('连接失败,原因:'+error);
          }
		    }
		  },
      async disconnectSerial(){	
        try{
        await this.reader.cancel(); 
        await this.reader.releaseLock();     
        await this.writer.close();
        await this.writer.releaseLock();
        await this.port.close();
        }catch(e){
          this.sendLabel=false;
          this.conButton='断开连接';
          this.conButtonType='warning';
          await this.sendQuery();
          this.$message.error('断开连接失败!');
          console.log(e);
        }
      }
  },
  data(){
    return{
      writer:{},
      port:{},
      reader:{},
      sendLabel:true,
      key1funcselectshow:false,
      key1mediaselectshow:false,
      key1input1show:false,
      key1input2show:false,
      key1radio : 0,
      key2funcselectshow:false,
      key2mediaselectshow:false,
      key2input1show:false,
      key2input2show:false,
      key2radio : 0,
      key3funcselectshow:false,
      key3mediaselectshow:false,
      key3input1show:false,
      key3input2show:false,
      key3radio : 0,
      conButton:"连接3 Key键盘",
      conButtonType:"primary",
      key1:0,
      key2:0,
      key3:0,
      key1func:0,
      key2func:0,
      key3func:0,
      key1:0,
      key2:0,
      key3:0,
      key1text1: '',
      key1text2: '',
      key2text1: '',
      key2text2: '',
      key3text1: '',
      key3text2: '',
      functionkeys:["KEY_LEFT_CTRL", "KEY_LEFT_SHIFT", "KEY_LEFT_ALT", "KEY_LEFT_GUI", "KEY_RIGHT_CTRL", "KEY_RIGHT_SHIFT", "KEY_RIGHT_ALT", "KEY_RIGHT_GUI", "KEY_UP_ARROW", "KEY_DOWN_ARROW", "KEY_LEFT_ARROW", "KEY_RIGHT_ARROW", "KEY_BACKSPACE", "KEY_TAB", "KEY_RETURN", "KEY_ESC", "KEY_INSERT", "KEY_DELETE", "KEY_PAGE_UP", "KEY_PAGE_DOWN", "KEY_HOME", "KEY_END", "KEY_CAPS_LOCK", "KEY_F1", "KEY_F2", "KEY_F3", "KEY_F4", "KEY_F5", "KEY_F6", "KEY_F7", "KEY_F8", "KEY_F9", "KEY_F10", "KEY_F11", "KEY_F12", "KEY_F13", "KEY_F14", "KEY_F15", "KEY_F16", "KEY_F17", "KEY_F18", "KEY_F19", "KEY_F20", "KEY_F21", "KEY_F22", "KEY_F23", "KEY_F24"],
      functionkeyvalues:[0x80, 0x81, 0x82, 0x83, 0x84, 0x85, 0x86, 0x87, 0xDA, 0xD9, 0xD8, 0xD7, 0xB2, 0xB3, 0xB0, 0xB1, 0xD1, 0xD4, 0xD3, 0xD6, 0xD2, 0xD5, 0xC1, 0xC2, 0xC3, 0xC4, 0xC5, 0xC6, 0xC7, 0xC8, 0xC9, 0xCA, 0xCB, 0xCC, 0xCD, 0xF0, 0xF1, 0xF2, 0xF3, 0xF4, 0xF5, 0xF6, 0xF7, 0xF8, 0xF9, 0xFA, 0xFB],
      mediakeys:["CONSUMER_POWER", "CONSUMER_SLEEP", "MEDIA_RECORD", "MEDIA_FAST_FORWARD", "MEDIA_REWIND", "MEDIA_NEXT", "MEDIA_PREVIOUS", "MEDIA_STOP", "MEDIA_PLAY_PAUSE", "MEDIA_PAUSE", "MEDIA_VOLUME_MUTE", "MEDIA_VOLUME_UP", "MEDIA_VOLUME_DOWN","CONSUMER_BRIGHTNESS_UP","CONSUMER_BRIGHTNESS_DOWN","CONSUMER_SCREENSAVER","CONSUMER_PROGRAMMABLE_BUTTON_CONFIGURATION","CONSUMER_CONTROL_CONFIGURATION","CONSUMER_EMAIL_READER","CONSUMER_CALCULATOR","CONSUMER_EXPLORER","CONSUMER_BROWSER_HOME","CONSUMER_BROWSER_BACK","CONSUMER_BROWSER_FORWARD","CONSUMER_BROWSER_REFRESH","CONSUMER_BROWSER_BOOKMARKS"],
      mediakeyvalues:[0x30, 0x32, 0xB2, 0xB3, 0xB4, 0xB5, 0xB6, 0xB7, 0xCD, 0xB0, 0xE2, 0xE9, 0xEA ,0x06F, 0x70F, 0x19e, 0x182, 0x183,0x18A,0x192,0x194,0x223,0x224,0x225,0x227,0x22A],
      key1mediaselectvalue: '',
      key1funcoptions: [],
      key1funcselectvalue: '',
      key1mediaoptions: [],
      key2funcoptions: [],
      key2funcselectvalue: '',
      key2mediaoptions: [],
      key2mediaselectvalue: '',
      key3mediaselectvalue: '',
      key3funcoptions: [],
      key3funcselectvalue: '',
      key3mediaoptions: [],
    }
  }
}
class LineBreakTransformer {
    constructor() {
        // 保存流数据直到新行出现的容器
        this.container = "";
    }

    transform(chunk, controller) {
        // 将新块追加到现有块。
        this.container += chunk;
        if(this.container=="1"){
          this.container="";
          controller.enqueue("1");          
        }
        else{
        // 对于每一行分段，将解析后的行发送出去。
        const lines = this.container.split("\r\n");
        this.container = lines.pop();
        lines.forEach((line) => controller.enqueue(line));
        }
    }

    flush(controller) {
        // 当流关闭时，清除所有剩余的块。
        controller.enqueue(this.container);
    }
}
</script>

<style scoped>
#app {
  font-family: Helvetica, sans-serif;
  text-align: center;
}
.el-row {
    margin-bottom: 20px;
  }
img {
    width: 500px;
}
</style>

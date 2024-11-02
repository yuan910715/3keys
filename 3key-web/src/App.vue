<template>
  <div id="app">
    <div>
       <el-row>      
          <img src="./assets/key1.png">
      </el-row>
      <el-row class="change-language">
        <el-button type="text" @click="changeLang('zh')" :class="[$i18n.locale === 'zh' ? 'light' : 'normal']">中文</el-button>
        <span>&nbsp;|&nbsp;</span>
        <el-button type="text" @click="changeLang('en')" :class="[$i18n.locale === 'en' ? 'light' : 'normal']">English</el-button>
      </el-row>
      <el-row>
      <el-popover
        placement="bottom"
        width="800"
        trigger="click">
        <div style="text-align: center;font-family: Helvetica, sans-serif;">
          <el-row  style="margin-bottom: 10px;"><h2>{{ $t('index.helpTitle') }}</h2></el-row>
          <el-row>{{ $t('index.helpTips0') }}</el-row>
          <el-row>{{ $t('index.helpTips1') }}<br/>{{ $t('index.helpTips1_1') }}</el-row>
          <el-row>{{ $t('index.helpTips2') }}<br/>{{ $t('index.helpTips2_1') }}</el-row>
          <el-row>{{ $t('index.helpTips3') }}</el-row>
          <el-row>{{ $t('index.helpTips4') }}</el-row>
          <el-row>{{ $t('index.helpTips5') }}</el-row>
          <el-row>{{ $t('index.helpTips6') }}</el-row>
        </div>
        <el-button slot="reference" type="info" icon="el-icon-chat-dot-round">{{ $t('index.helpButton') }}</el-button>
      </el-popover>
      </el-row>
      <el-row>      
        <el-button :type="conButtonType" @click="conClick()" icon="el-icon-cpu">{{ conButton }}</el-button>       
      </el-row>
      <el-row>
        <key-config ref="key1config" labelstr="key1" :stringshow="true"></key-config>
      </el-row>
      <el-row>
        <key-config ref="key2config" labelstr="key2" :stringshow="true"></key-config>
      </el-row>
      <el-row style="margin-bottom: 5px;">
        <key-config ref="key3config" labelstr="key3" :stringshow="true"></key-config>
      </el-row>
      <el-row style="margin-bottom: 30px;">
       <label>{{ $t('index.keyAllLabel') }}</label><el-switch style="margin-left: 20px;" v-model="keyallmodeshow" :active-text="$t('index.keyAllTrue')" :inactive-text="$t('index.keyAllFalse')"></el-switch>
      </el-row>
      <el-row v-show="keyallmodeshow">
        <key-config ref="keyallconfig" labelstr="keyAll" :stringshow="false"></key-config>
      </el-row> 
      <el-row>
        <el-button type="primary" @click="sendClick()" :disabled="sendLabel">{{sendText}}</el-button>   
      </el-row>
      <el-row>
        <img src="./assets/key2.png">
      </el-row>
      <el-row>
        <p>{{ $t('index.changeLog5') }} <a :href="$t('index.howtoaddress')" target="_blank">{{ $t('index.changeLog1_1') }}</a></p>
        <p>{{ $t('index.changeLog4') }}</p>
        <p>{{ $t('index.changeLog3') }}</p>
        <p>{{ $t('index.changeLog1') }}</p>
        <p>{{ $t('index.changeLog2') }}</p>
      </el-row>
      <el-row>
        <p><a href="https://www.bilibili.com/video/BV1M84y1n7r1" target="_blank">{{ $t('index.videoText') }}</a></p>
        <p>{{ $t('index.keyTest') }}</p>
        <p><a href="https://keyboard.bmcx.com/" target="_blank">https://keyboard.bmcx.com/</a></p>
        <p>{{ $t('index.mouseTest') }}</p>
        <p><a href="https://cps-check.com/cn/mouse-buttons-test" target="_blank">https://cps-check.com/cn/mouse-buttons-test</a></p>       
      </el-row>
      <el-row>
        {{ $t('index.openSource') }}
      </el-row>
      <el-row>
        <a href="https://github.com/yuan910715" target="_blank">https://github.com/yuan910715</a>
      </el-row>
      <el-row>
        <a href="https://topyuan.top">{{ $t('index.backToIndex') }}</a>
      </el-row> 
    </div>
  </div>
</template>

<script>
import KeyConfig from './components/KeyConfig.vue';
import axios from 'axios';
export default {
  components: { KeyConfig },
  methods: {   
    conClick(){     
      if(this.sendLabel==false){
        this.sendLabel=true;
        this.sendText=this.$t('index.connFirst');
        this.conButton=this.$t('index.connButtonText1');
        this.conButtonType="primary";
        this.$refs.key1config.keyReset();
        this.$refs.key2config.keyReset();
        this.$refs.key3config.keyReset();
        this.$refs.keyallconfig.keyReset();
        this.keyallmodeshow=false;
        this.disconnectSerial();
      }
      else if (navigator.serial) {
		     this.connectSerial();
		   } else {
         alert(this.$t('index.browserErr'));
		   }
    },
    async sendCommit(){
      const data = new Uint8Array([0x02, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                    0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                    0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                    0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                    0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                    0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0x00, 0x03 ]);
     let r1=this.$refs.key1config.getResult();
     if(!r1){
        return;
     }
     let r2=this.$refs.key2config.getResult();
     if(!r2){
        return;
     }
     let r3=this.$refs.key3config.getResult();
     if(!r3){
        return;
     }
     if(this.keyallmodeshow){
        let rall=this.$refs.keyallconfig.getResult();
        if(!rall){
            return;
        }
        data[115]=rall.keyCode;
        data[116]=rall.funcCode;
        data[117]=rall.mediaExtraCode;
        data[153]=0x01;
        for(var i=0;i<30;i++){
          data[118+i]=rall.stringCode[i];
        }
        for(var i=0;i<5;i++){
          data[148+i]=rall.mixCode[i];
        }
        if(this.oldversion){
          if(rall.funcCode==0x04 || rall.funcCode==0x05 ||rall.funcCode==0x06 ||rall.funcCode==0x07 ||rall.funcCode==0x08 ||rall.funcCode==0x09 ){
            this.$confirm(this.$t('index.firmwareErr'),  this.$t('index.tip'), {
                    dangerouslyUseHTMLString: true,
                    confirmButtonText: this.$t('index.updateHelp'),
                    cancelButtonText: this.$t('index.updateNo'),
                    type: 'warning'
                  }).then(() => {
                    this.$message({
                      type: 'success',
                      message: this.$t('index.redirecting')
                    });
                    window.location.href=this.$t('index.howtoaddress');
                  }).catch(() => {
                    return;
                  });
            return;
          }
        }
        if(this.v5version){
          if( (r1.funcCode==0x03&&r1.mediaExtraCode==0x01) || (r2.funcCode==0x03&&r2.mediaExtraCode==0x01) || (r3.funcCode==0x03&&r3.mediaExtraCode==0x01) ){
            this.$confirm(this.$t('index.firmwareErrv5'),  this.$t('index.tip'), {
                    dangerouslyUseHTMLString: true,
                    confirmButtonText: this.$t('index.updateHelp'),
                    cancelButtonText: this.$t('index.updateNo'),
                    type: 'warning'
                  }).then(() => {
                    this.$message({
                      type: 'success',
                      message: this.$t('index.redirecting')
                    });
                    window.location.href=this.$t('index.howtoaddress');
                  }).catch(() => {
                    return;
                  });
            return;
          }
        }
        if(!this.v7version){          
          if( (r1.funcCode==0x04&&r1.mediaExtraCode==0x02) || (r2.funcCode==0x04&&r2.mediaExtraCode==0x02) || (r3.funcCode==0x04&&r3.mediaExtraCode==0x02) || (keyall.funcCode==0x04&&this.keyall.mediaExtraCode==0x02) ){
            this.$confirm(this.$t('index.firmwareErrv6'),  this.$t('index.tip'), {
                    dangerouslyUseHTMLString: true,
                    confirmButtonText: this.$t('index.updateHelp'),
                    cancelButtonText: this.$t('index.updateNo'),
                    type: 'warning'
                  }).then(() => {
                    this.$message({
                      type: 'success',
                      message: this.$t('index.redirecting')
                    });
                    window.location.href=this.$t('index.howtoaddress');
                  }).catch(() => {
                    return;
                  });
            return;
          }
        }
     }
     if(this.oldversion){
      if(r1.funcCode==0x04 || r1.funcCode==0x05 ||r1.funcCode==0x06 ||r1.funcCode==0x07 ||r1.funcCode==0x08 ||r1.funcCode==0x09 ||
      r2.funcCode==0x04 || r2.funcCode==0x05 ||r2.funcCode==0x06 ||r2.funcCode==0x07 ||r2.funcCode==0x08 ||r2.funcCode==0x09 ||
      r3.funcCode==0x04 || r3.funcCode==0x05 ||r3.funcCode==0x06 ||r3.funcCode==0x07 ||r3.funcCode==0x08 ||r3.funcCode==0x09 ){
        this.$confirm(this.$t('index.firmwareErr'), this.$t('index.tip'), {
                    dangerouslyUseHTMLString: true,
                    confirmButtonText: this.$t('index.updateHelp'),
                    cancelButtonText: this.$t('index.updateNo'),
                    type: 'warning'
                  }).then(() => {
                    this.$message({
                      type: 'success',
                      message: this.$t('index.redirecting')
                    });
                    window.location.href=this.$t('index.howtoaddress');
                  }).catch(() => {
                    return;
                  });
            return;
      }      
     }
     data[1]=r1.keyCode;
     data[4]=r1.funcCode;
     data[7]=r1.mediaExtraCode;
     data[2]=r2.keyCode;
     data[5]=r2.funcCode;
     data[8]=r2.mediaExtraCode;
     data[3]=r3.keyCode;
     data[6]=r3.funcCode;
     data[9]=r3.mediaExtraCode;
     for(var i=0;i<30;i++){
      data[10+i]=r1.stringCode[i];
      data[40+i]=r2.stringCode[i];
      data[70+i]=r3.stringCode[i];
     }
     for(var i=0;i<5;i++){
      data[100+i]=r1.mixCode[i];
      data[105+i]=r2.mixCode[i];
      data[110+i]=r3.mixCode[i];
     }
      //console.log(data);
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
          this.reader = decoder.readable.pipeThrough(new TransformStream(new LineBreakTransformer())).getReader();
		      this.sendLabel=false;
          this.sendText=this.$t('index.submit');
          this.conButton=this.$t('index.disconnect');
          this.conButtonType='warning';
          await this.sendQuery();
          let label=0;
		      while (true) {
		        const { value, done } = await this.reader.read();
               if(label==0){
                if(value=="1"){
                  this.$message({
                    message:this.$t('index.success'),
                    type:"success"
                  });
                  label=0;
                  continue;
                }else if(value && value!='v3' && value!='v4' && value!='v5' && value!='v6'&& value!='v7'){
                  await this.disconnectSerial();
                  this.sendLabel=true;
                  this.sendText=this.sendText=this.$t('index.submit');
                  this.conButton="this.$t('index.connButtonText1')";
                  this.conButtonType="primary";
                  this.$confirm(this.$t('index.firmwareErr1'),  this.$t('index.tip'), {
                    dangerouslyUseHTMLString: true,
                    confirmButtonText: this.$t('index.updateHelp'),
                    cancelButtonText: this.$t('index.updateNo1'),
                    type: 'warning'
                  }).then(() => {
                    this.$message({
                      type: 'success',
                      message: this.$t('index.redirecting')
                    });
                    window.location.href=this.$t('index.howtoaddress');
                  }).catch(() => {
                    this.$message({
                      type: 'success',
                      message: this.$t('index.redirecting')
                    });
                    window.location.href="https://topyuan.top/3keyold";
                  });
               
                  return;
                }else if(value=='v3' || value=='v4'){
                  this.oldversion = true;
                }else if(value=='v5'){
                  this.v5version = true;
                }else if(value=='v7'){
                  this.v7version = true;
                }
               }else if(label==1){
                this.key1=parseInt(value,16);
               }else if(label==2){
                this.key2=parseInt(value,16);             
               }else if(label==3){
                this.key3=parseInt(value,16);
               }else if(label==4){
                this.key1func=parseInt(value,16);
               }else if(label==5){
                this.key2func=parseInt(value,16);
               }else if(label==6){
                this.key3func=parseInt(value,16);
               }else if(label==7){
                this.key1f=parseInt(value,16);
               }else if(label==8){
                this.key2f=parseInt(value,16);
               }else if(label==9){
                this.key3f=parseInt(value,16);
               }else if(label==10){
                this.key1str=value;
               }else if(label==11){
                this.key2str=value;
               }else if(label==12){
                this.key3str=value;
               }else if(label==13){
                this.key1mix[0]=parseInt(value,16);
               }else if(label==14){
                this.key1mix[1]=parseInt(value,16);
               }else if(label==15){
                this.key1mix[2]=parseInt(value,16);
               }else if(label==16){
                this.key1mix[3]=parseInt(value,16);
               }else if(label==17){
                this.key1mix[4]=parseInt(value,16);
               }else if(label==18){
                this.key2mix[0]=parseInt(value,16);
               }else if(label==19){
                this.key2mix[1]=parseInt(value,16);
               }else if(label==20){
                this.key2mix[2]=parseInt(value,16);
               }else if(label==21){
                this.key2mix[3]=parseInt(value,16);
               }else if(label==22){
                this.key2mix[4]=parseInt(value,16);
               }else if(label==23){
                this.key3mix[0]=parseInt(value,16);
               }else if(label==24){
                this.key3mix[1]=parseInt(value,16);
               }else if(label==25){
                this.key3mix[2]=parseInt(value,16);
               }else if(label==26){
                this.key3mix[3]=parseInt(value,16);
               }else if(label==27){
                this.key3mix[4]=parseInt(value,16);
               }else if(label==28){
                this.keyall=parseInt(value,16);
               }else if(label==29){
                this.keyallfunc=parseInt(value,16);
               }else if(label==30){
                this.keyallf=parseInt(value,16);
               }else if(label==31){
                this.keyallstr=value;
               }else if(label==32){
                this.keyallmix[0]=parseInt(value,16);
               }else if(label==33){
                this.keyallmix[1]=parseInt(value,16);
               }else if(label==34){
                this.keyallmix[2]=parseInt(value,16);
               }else if(label==35){
                this.keyallmix[3]=parseInt(value,16);
               }else if(label==36){
                this.keyallmix[4]=parseInt(value,16);
               }else if(label==37){
                this.keyallmode=parseInt(value,16);
               }              
		        label++;
            if(label>=38){
              label=0;
              this.$refs.key1config.setKey(this.key1,this.key1func,this.key1f,this.key1str,this.key1mix);
              this.$refs.key2config.setKey(this.key2,this.key2func,this.key2f,this.key2str,this.key2mix);
              this.$refs.key3config.setKey(this.key3,this.key3func,this.key3f,this.key3str,this.key3mix);
              if(this.keyallmode==0x01){
                this.keyallmodeshow=true;
                this.$refs.keyallconfig.setKey(this.keyall,this.keyallfunc,this.keyallf,this.keyallstr,this.keyallmix);
              }else{
                this.keyallmodeshow=false;
              }
            }
		        if (done) {
              await readableStreamClosed.catch(()=>{});       
		          break;
		        }
		      }
		    } catch (error) {
          if(error.name=='NotFoundError'){
            return;
          }
          else if(error.name=='InvalidStateError'||error.name=='NetworkError'){
            this.$message.error(this.$t('index.conError1'));
          }
          else{
            this.$message.error(this.$t('index.conError2')+error);
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
          this.sendText=this.$t('index.submit');
          this.conButton=this.$t('index.disconnect');
          this.conButtonType='warning';
          // await this.sendQuery();
          this.$message.error(this.$t('index.conError3'));
          console.log(e);
        }
      },
      getCommonData(){
        var this_app=this;
        axios.get('common.json')
        .then(function (response) {
          let commonkeys=[];
          for(var i=0;i<response.data.length;i++){
            let tmp={};
            if(!tmp.mixkeys){
              tmp.mixkeys=[];
            }
            for(var j=0;j<response.data[i].mixkeys.length;j++){
               tmp.mixkeys[j]=(parseInt(response.data[i].mixkeys[j]));
            }
            if(this_app.$i18n.locale=='zh'){
              tmp.description=response.data[i].description_zh;
            }else if(this_app.$i18n.locale=='en'){
              tmp.description=response.data[i].description_en;
            }
            commonkeys.push(tmp);          
          }
          this_app.$refs.key1config.setCommon(commonkeys);
          this_app.$refs.key2config.setCommon(commonkeys);
          this_app.$refs.key3config.setCommon(commonkeys);
          this_app.$refs.keyallconfig.setCommon(commonkeys);
        })
        .catch(function (error) {
          console.log(error);
        });
      },
      changeLang(lang){        
        this.$i18n.locale = lang;
        localStorage.setItem('lang',lang);      
        this.$refs.key1config.refreshText();
        this.$refs.key2config.refreshText();
        this.$refs.key3config.refreshText();
        this.$refs.keyallconfig.refreshText();
        this.getCommonData();
        if(this.sendLabel==true){
          this.sendText=this.$t('index.connFirst');
          this.conButton=this.$t('index.connButtonText1');
        }else{
          this.sendText=this.$t('index.submit');
          this.conButton=this.$t('index.disconnect');
        }
      }
  },
  mounted (){
    this.getCommonData();    
  },
  data(){
    return{
      v5version:false,
      v7version:false,
      oldversion:false,
      keyallmodeshow:false,
      writer:{},
      port:{},
      reader:{},
      sendLabel:true,
      sendText:this.$t('index.connFirst'),
      conButton:this.$t('index.connButtonText1'),
      conButtonType:"primary",
      key1:0xff,
      key2:0xff,
      key3:0xff,
      key1func:0xff,
      key2func:0xff,
      key3func:0xff,
      key1f:0xff,
      key2f:0xff,
      key3f:0xff,
      key1str:'',
      key2str:'',
      key3str:'',
      key1mix:[0xff,0xff,0xff,0xff,0xff],
      key2mix:[0xff,0xff,0xff,0xff,0xff],
      key3mix:[0xff,0xff,0xff,0xff,0xff],
      keyall:0xff,
      keyallfunc:0xff,
      keyallf:0xff,
      keyallstr:'',
      keyallmix:[0xff,0xff,0xff,0xff,0xff],
      keyallmode:0xff
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
        // console.log(this.container);
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
.change-language {
  display: flex;
  justify-content: center;
  align-items: center;
}
.el-row {
    margin-bottom: 20px;
  }
img {
    width: 500px;
}
.light {
  color: #409EFF;
}
.normal {
  color: black;
}
</style>

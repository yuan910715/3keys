<template>
    <div>
        <el-row>
        <label style="margin-right: 20px;" v-html="keyname"></label>
        <el-radio-group v-model="keyradio" @change="fucChange()">
          <el-radio :label="1">{{ $t('keyConfig.keyDes1') }}</el-radio>
          <el-radio :label="2">{{ $t('keyConfig.keyDes2') }}</el-radio>
          <el-radio :label="3">{{ $t('keyConfig.keyDes3') }}</el-radio>
          <el-radio :label="4">{{ $t('keyConfig.keyDes4') }}</el-radio>
          <el-radio :label="5" v-if="stringshow">{{ $t('keyConfig.keyDes5') }}</el-radio>
          <el-radio :label="6">{{ $t('keyConfig.keyDes6') }}</el-radio>
        </el-radio-group>
      </el-row>
      <el-row>
        <el-select v-model="funcselectvalue" :placeholder="$t('keyConfig.select')" v-if="funcselectshow" style="width:200px">
          <el-option
            v-for="item in funcoptions"
            :key="item.value"
            :label="item.label"
            :value="item.value">
          </el-option>
        </el-select>
        <el-select v-model="mediaselectvalue" :placeholder="$t('keyConfig.select')" v-if="mediaselectshow" style="width:200px">
          <el-option
            v-for="item in mediaoptions"
            :key="item.value"
            :label="item.label"
            :value="item.value">
          </el-option>
        </el-select>
        <el-input style="width:70px" type="text" v-model="text1" maxlength="1" show-word-limit v-if="input1show"></el-input>       
        <el-input style="width:350px" type="text" v-model="text2" maxlength="30" show-word-limit clearable v-if="input2show"></el-input>      
        <div v-if="mouseshow">
        <el-select v-model="mouseselectvalue" :placeholder="$t('keyConfig.select')" v-if="mouseselectshow" style="width:200px" @change="mousecheckchange()">
          <el-option
            v-for="item in mouseoptions"
            :key="item.value"
            :label="item.label"
            :value="item.value">
          </el-option>
        </el-select>
        <label style="margin-left: 20px;">{{ mouseinputtxt }}</label>
        <el-input-number v-if="mouseinputshow" v-model="mouseinput" :step="1" :min=1 :max=120></el-input-number>
        <el-input-number v-if="mousescroolshow" v-model="mousescrool" :step="1" :min=1 :max=5></el-input-number>
        <el-checkbox v-if="mousecheckshow" v-model="mousecheck" style="margin-left: 20px;" >{{ mousechecktxt }}</el-checkbox>
        </div>
      </el-row>
      <div v-if="mixshow">
      <el-row v-if="mixcountarray[0]">
        <el-select v-model="mixfuncselectvalue[0]" :placeholder="$t('keyConfig.select')" style="width:200px" v-if="mixshowarray[0]">
          <el-option
            v-for="item in mixfuncoptions"
            :key="item.value"
            :label="item.label"
            :value="item.value">
          </el-option>
        </el-select>
        <el-input style="width:70px" type="text" v-model="mixtext[0]" maxlength="1" show-word-limit v-if="!mixshowarray[0]"></el-input>
        <div style="display:inline-block;" v-if="mixcountarray[1]">
        +
          <el-select v-model="mixfuncselectvalue[1]" :placeholder="$t('keyConfig.select')" style="width:200px"  v-if="mixshowarray[1]">
          <el-option
            v-for="item in mixfuncoptions"
            :key="item.value"
            :label="item.label"
            :value="item.value">
          </el-option>
        </el-select>
        <el-input style="width:70px" type="text" v-model="mixtext[1]" maxlength="1" show-word-limit v-if="!mixshowarray[1]"></el-input>  
        </div>
        <div style="display:inline-block;" v-if="mixcountarray[2]">
        +
        <el-select v-model="mixfuncselectvalue[2]" :placeholder="$t('keyConfig.select')" style="width:200px" v-if="mixshowarray[2]">
          <el-option
            v-for="item in mixfuncoptions"
            :key="item.value"
            :label="item.label"
            :value="item.value">
          </el-option>
        </el-select>
        <el-input style="width:70px" type="text" v-model="mixtext[2]" maxlength="1" show-word-limit v-if="!mixshowarray[2]"></el-input> 
        </div>
      </el-row>
      <el-row v-if="mixcountarray[3]">
        +
        <el-select v-model="mixfuncselectvalue[3]" :placeholder="$t('keyConfig.select')" style="width:200px" v-if="mixshowarray[3]">
          <el-option
            v-for="item in mixfuncoptions"
            :key="item.value"
            :label="item.label"
            :value="item.value">
          </el-option>
        </el-select>
        <el-input style="width:70px" type="text" v-model="mixtext[3]" maxlength="1" show-word-limit v-if="!mixshowarray[3]"></el-input>    
        <div style="display:inline-block;" v-if="mixcountarray[4]">
        +
        <el-select v-model="mixfuncselectvalue[4]" :placeholder="$t('keyConfig.select')" style="width:200px" v-if="mixshowarray[4]">
          <el-option
            v-for="item in mixfuncoptions"
            :key="item.value"
            :label="item.label"
            :value="item.value">
          </el-option>
        </el-select>
        <el-input style="width:70px" type="text" v-model="mixtext[4]" maxlength="1" show-word-limit v-if="!mixshowarray[4]"></el-input>  
        </div>
      </el-row>
      <el-row><label v-html="mixtips"></label></el-row>
      <el-row>        
        <el-button type="success" @click="mixadd(1)">{{ $t('keyConfig.mix1') }}</el-button>
        <el-button type="success" @click="mixadd(2)">{{ $t('keyConfig.mix2') }}</el-button>
        <el-button type="danger" @click="mixdelete()">{{ $t('keyConfig.mix3') }}</el-button>
        <el-button type="danger" @click="mixreset()">{{ $t('keyConfig.mix4') }}</el-button>
        <el-popover
        ref="popover"
        placement="left"
        width="630"
        trigger="click"
        v-show="commonshow">
        <div style="text-align: center;font-family: Helvetica, sans-serif;">
          <el-row style="margin-bottom: 10px;"><h4>{{ $t('keyConfig.mixtip') }}</h4></el-row>
          <el-row style="margin-bottom: 10px;">
          <el-table
          :data="tableData"
          height="600"
          style="width: 120%;"
          :header-cell-style="{textAlign: 'center'}"
          :cell-style="{textAlign: 'center'}" >
          <el-table-column
            prop="keys"
            :label="$t('keyConfig.mixcommon1')"
            width="250">
          </el-table-column>
          <el-table-column
            prop="description"
            :label="$t('keyConfig.mixcommon2')"
            width="250">
          </el-table-column>
          <el-table-column
            :label="$t('keyConfig.mixcommon3')"
            width="130">
            <template slot-scope="scope">
              <el-button type="success" size="small" @click="handleCommonClick(scope.row)">{{ $t('keyConfig.set') }}</el-button>
            </template>
          </el-table-column>
          </el-table>       
         </el-row>
        </div>        
        <el-button slot="reference" type="info" icon="el-icon-paperclip" style="margin-left: 10px;">{{ $t('keyConfig.mix5') }}</el-button>
      </el-popover>
      </el-row>
      </div>
    </div>
</template>

<script>
export default{
    props:{
        labelstr:'',        
        stringshow : true
    },
    data(){
        return {
            keyname:'',
            keyradio:0,
            funcselectshow:false,
            mediaselectshow:false,
            input1show:false,
            input2show:false,
            mouseselectshow:false,
            mouseselectvalue:'',
            mouseoptions:[],
            mixshow:false,
            mediaselectvalue: '',
            funcoptions: [],
            funcselectvalue: '',
            mediaoptions: [],
            text1: '',
            text2: '',
            mixcount:0,
            mousescroolshow:false,
            mousescrool:0,
            mouseinputtxt:'',
            mouseshow:false,
            mousechecktxt:'',
            mouseinputshow:false,
            mousecheckshow:false,
            mousecheck:false,
            mouseinput:10,
            mixfuncoptions: [],
            mixfuncselectvalue: ['','','','',''],
            mixtext:['','','','',''],
            mixtips:'',
            mixcountarray:[false,false,false,false,false],
            mixshowarray:[false,false,false,false,false],
            mouses:this.$t('keyConfig.mouses'),
            functionkeys:this.$t('keyConfig.functionkeys'),
            functionkeyvalues:[0x80, 0x81, 0x82, 0x83, 0x84, 0x85, 0x86, 0x87, 0xDA, 0xD9, 0xD8, 0xD7, 0xB2, 0xB3, 0xB0, 0xB1, 0xD1, 0xD4, 0xD3, 0xD6, 0xD2, 0xD5, 0xC1, 0xCE, 0xCF, 0xD0, 0xDB, 0xC2, 0xC3, 0xC4, 0xC5, 0xC6, 0xC7, 0xC8, 0xC9, 0xCA, 0xCB, 0xCC, 0xCD, 0xF0, 0xF1, 0xF2, 0xF3, 0xF4, 0xF5, 0xF6, 0xF7, 0xF8, 0xF9, 0xFA, 0xFB],
            mediakeys:this.$t('keyConfig.mediakeys'),
            mediakeyvalues:[0x30, 0x32, 0xB2, 0xB3, 0xB4, 0xB5, 0xB6, 0xB7, 0xCD, 0xB0, 0xE2, 0xE9, 0xEA ,0x06F, 0x70, 0x19e, 0x183,0x18A,0x192,0x194,0x223,0x224,0x225,0x227,0x22A],
            tableData: [],
            commonshow:false
        }
    },
    mounted() {
        this.keyname = this.labelstr + ':';
    },
    methods:{
         setCommon(commonkeys){
          this.tableData.splice(0,this.tableData.length);
          for(var i=0;i<commonkeys.length;i++){
          let item ={};
          let keys='';
          for(var j=0;j<commonkeys[i].mixkeys.length;j++){
            let keylabel=false;
            for(var k=0;k<this.functionkeyvalues.length;k++){
                if(this.functionkeyvalues[k]==commonkeys[i].mixkeys[j]){
                  if(j!=0){
                    keys+='+ '
                  }
                  keys+=this.functionkeys[k]+' ';
                    keylabel=true;
                 }
              }
            if(!keylabel)          
            {
              if(j!=0){
                keys+='+ '
              }
              keys+=String.fromCharCode(commonkeys[i].mixkeys[j])+' ';              
            }
          }
          item.keys=keys;
          item.description=commonkeys[i].description;
          item.mixkeys=commonkeys[i].mixkeys;
          this.tableData.push(item);
        }
        if(commonkeys.length){
          this.commonshow=true;
        }
        },
        handleCommonClick(row){
          this.mixcount=row.mixkeys.length;
          this.mixcountchange();
          for(var i=0;i<row.mixkeys.length;i++){
            let keylabel=false;
            for(var k=0;k<this.functionkeyvalues.length;k++){
                if(this.functionkeyvalues[k]==row.mixkeys[i]){        
                    this.mixfuncselectvalue[i]=row.mixkeys[i];
                    keylabel=true;
                    this.mixshowarray[i]=true;
                 }
              }
            if(!keylabel)          
            {              
              this.mixtext[i]=String.fromCharCode(row.mixkeys[i]);
              this.mixshowarray[i]=false;         
            }
          }
          this.$refs.popover.doClose();
        },
        keyReset(){
          this.keyradio=0;
          this.funcselectshow=false;
          this.mediaselectshow=false;
          this.input1show=false;
          this.input2show=false;
          this.mixshow=false;
          this.mouseselectshow=false,
          this.mouseselectvalue='',
          this.mouseoptions=[],
          this.mediaselectvalue= '';
          this.funcoptions= [];
          this.funcselectvalue= '';
          this.mediaoptions= [];
          this.text1= '';
          this.text2= '';
          this.mousescroolshow=false,
          this.mousescrool=0,
          this.mouseinputtxt='',
          this.mouseshow=false,
          this.mousechecktxt='',
          this.mouseinputshow=false,
          this.mousecheckshow=false,
          this.mousecheck=false,
          this.mouseinput=10,
          this.mixcount=0;
          this.mixfuncoptions=[];
          this.mixfuncselectvalue=['','','','',''];
          this.mixtext=['','','','',''];
          this.mixtips='';
          this.mixcountarray=[false,false,false,false,false];
          this.mixshowarray=[false,false,false,false,false];
        },
        setKey(key,keyfunc,keyf,customstr,keymix){
          switch(keyfunc){
                case 1://media
                  this.keyradio=3;
                  this.fucChange();
                  if(keyf==0xff){
                   this.mediaselectvalue=key;
                  }else{
                    let tmp=Number(keyf<<8)+Number(key);
                    this.mediaselectvalue=tmp;              
                  }
                  break;
                case 2://mix
                  this.mixcount=0;
                  this.keyradio=4;
                  this.fucChange();
                  for(var i=0;i<keymix.length;i++){
                      if(keymix[i]!=0xff){
                        this.mixcount++;
                        let tmplabel=true;
                        for(var j=0;j<this.functionkeyvalues.length;j++){
                          if(this.functionkeyvalues[j]==keymix[i]){
                            this.mixfuncselectvalue[i]=keymix[i];
                            this.mixshowarray[i]=true;
                            tmplabel=false;
                            break;
                          }
                        }
                        if(tmplabel){
                        this.mixshowarray[i]=false;
                        this.mixtext[i]=String.fromCharCode(keymix[i]);
                        }
                      }
                  }
                  this.mixcountchange();
                  break;
                case 3://string
                  this.keyradio=5;
                  this.fucChange();
                  this.text2=customstr;
                  break;
                case 4: //mouse click
                  this.keyradio=6;
                  if(keyf==0x00){
                    this.mousecheck = false;
                  }else if(keyf==0x01){
                    this.mousecheck = true;
                  }
                  if(key==0x01){
                    this.mouseselectvalue=1;
                  }else if(key==0x02){
                    this.mouseselectvalue=3;
                  }else if(key==0x04){
                    this.mouseselectvalue=2;
                  }                  
                  this.fucChange();       
                  break;
                case 5: //mouse move random
                  this.keyradio=6;       
                  this.mouseselectvalue=4;     
                  this.fucChange();       
                  break;
                case 6: //mouse move
                  this.keyradio=6;       
                  if(keyf==0x01){//up
                    this.mouseselectvalue=5;  
                  }else if(keyf==0x02){//down
                    this.mouseselectvalue=6;  
                  }else if(keyf==0x03){//left
                    this.mouseselectvalue=7;  
                  }else if(keyf==0x04){//right
                    this.mouseselectvalue=8;  
                  }                 
                  this.mouseinput=(key-31);
                  this.mousecheck = false;
                  this.fucChange();       
                  break;
                case 7: //mouse move hold
                  this.keyradio=6;       
                  if(keyf==0x01){//up
                    this.mouseselectvalue=5;  
                  }else if(keyf==0x02){//down
                    this.mouseselectvalue=6;  
                  }else if(keyf==0x03){//left
                    this.mouseselectvalue=7;  
                  }else if(keyf==0x04){//right
                    this.mouseselectvalue=8;  
                  }                 
                  this.mouseinput=(key-31);
                  this.mousecheck = true;
                  this.fucChange();       
                  break;
                case 8: //mouse scrool
                  this.keyradio=6;       
                  if(keyf==0x01){//up
                    this.mouseselectvalue=9;  
                  }else if(keyf==0x02){//down
                    this.mouseselectvalue=10;  
                  }         
                  if(key==1){
                  this.mousescrool=1;
                  }else if(key==5){
                  this.mousescrool=2;
                  }else if(key==20){
                  this.mousescrool=3;
                  }else if(key==60){
                  this.mousescrool=4;
                  }else if(key==100){
                  this.mousescrool=5;
                  }
                  this.fucChange();       
                  break;
                case 9: //mouse scrool hold
                  this.keyradio=6;       
                  if(keyf==0x01){//up
                    this.mouseselectvalue=9;  
                  }else if(keyf==0x02){//down
                    this.mouseselectvalue=10;  
                  }              
                  if(key==1){
                  this.mousescrool=1;
                  }else if(key==5){
                  this.mousescrool=2;
                  }else if(key==20){
                  this.mousescrool=3;
                  }else if(key==60){
                  this.mousescrool=4;
                  }else if(key==100){
                  this.mousescrool=5;
                  }
                  this.mousecheck = true;
                  this.fucChange();       
                  break;
                default://normal
                  let keylabel=false;
                  //function
                  for(var i=0;i<this.functionkeyvalues.length;i++){
                    if(this.functionkeyvalues[i]==key){
                      this.keyradio=2;
                      this.fucChange();
                      this.funcselectvalue=key;
                      keylabel=true;
                    }
                  }
                  if(!keylabel){//normal
                    this.keyradio=1;
                    this.fucChange();
                    this.text1=String.fromCharCode(key);
                  }
                  break;
              }
        },
        getResult(){
            let result={
                funcCode:0xff,
                keyCode:0xff,
                mediaExtraCode:0xff,
                mixCode:[0xff,0xff,0xff,0xff,0xff],
                stringCode:[0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff]
            };
            switch(this.keyradio){
          case 1://normal
              if(this.text1.length!=1){
                this.$message.error(this.labelstr+this.$t('keyConfig.inputErr'));
                return;
              }
              let tmp=this.text1.charCodeAt(0);
                if(tmp<0||tmp>255){
                  this.$message.error(this.labelstr+this.$t('keyConfig.inputErr'));
                  return;
                }                
                result.keyCode=tmp;
                result.funcCode=0x00;                     
              break;
          case 2://function
              if(this.funcselectvalue==""){
                this.$message.error(this.labelstr+this.$t('keyConfig.selectErr'));
                return;
              }          
                result.keyCode=this.funcselectvalue;
                result.funcCode=0x00;     
              break;
          case 3://media
              if(this.mediaselectvalue==""){
                this.$message.error(this.labelstr+this.$t('keyConfig.selectErr'));
                return;
              }          
                result.keyCode=this.mediaselectvalue & 0xff;
                if(((this.mediaselectvalue>>8) & 0xff) !=0){
                  result.mediaExtraCode = (this.mediaselectvalue>>8) & 0xff;
                }
                result.funcCode=0x01;     
              break;
          case 4://mix
                if(this.mixcount<2){
                  this.$message.error(this.labelstr+this.$t('keyConfig.mixErr'));
                  return;
                }
                for(var i=0;i<this.mixcount;i++){
                    if(this.mixshowarray[i]){
                      if(this.mixfuncselectvalue[i]==""){
                        this.$message.error(this.labelstr+this.$t('keyConfig.mixErr1')+(i+1)+this.$t('keyConfig.mixErr2'));
                        return;
                      }  
                      result.mixCode[i]=this.mixfuncselectvalue[i];
                    }else{
                      if(this.mixtext[i].length!=1){
                        this.$message.error(this.labelstr+this.$t('keyConfig.mixErr1')+(i+1)+this.$t('keyConfig.mixErr3'));
                        return;
                      }
                      let tmp=this.mixtext[i].charCodeAt(0);
                      if(tmp<0||tmp>255){
                        this.$message.error(this.labelstr+this.$t('keyConfig.mixErr1')+(i+1)+this.$t('keyConfig.mixErr3'));
                        return;
                      }  
                      result.mixCode[i]=tmp;
                    }
                }                
                result.funcCode=0x02;
            break; 
          case 5://string
            if(this.text2.length==0){
                this.$message.error(this.labelstr+this.$t('keyConfig.textErr'));
                return;            
            }
            for(var i=0;i<this.text2.length;i++){
              if(this.text2.charCodeAt(i)<0||this.text2.charCodeAt(i)>255){
                this.$message.error(this.labelstr+this.$t('keyConfig.textErr1'));
                return;
              }
              result.stringCode[i]=this.text2.charCodeAt(i);
            }
            result.funcCode=0x03;
            break;
         case 6://mouse
              if(this.mouseselectvalue==""){
                this.$message.error(this.labelstr+this.$t('keyConfig.selectErr'));
                return;
              }
              if(this.mouseselectvalue==1 || this.mouseselectvalue==2 || this.mouseselectvalue==3 ){ //click
                  result.mediaExtraCode= this.mousecheck?0x01:0x00;
                  if(this.mouseselectvalue==1){//left
                    result.keyCode=0x01;
                    result.funcCode=0x04; 
                  }else if(this.mouseselectvalue==2){//middle
                    result.keyCode=0x04;
                    result.funcCode=0x04;
                  }else if(this.mouseselectvalue==3){//right
                    result.keyCode=0x02;
                    result.funcCode=0x04;
                  }
             }else if(this.mouseselectvalue==4){ //move random
                  result.funcCode=0x05;
             }else if(this.mouseselectvalue==5 || this.mouseselectvalue==6 || this.mouseselectvalue==7 || this.mouseselectvalue==8 ){ //move move 
                  result.funcCode= this.mousecheck?0x07:0x06;
                  if(this.mouseselectvalue==5){//up
                    result.mediaExtraCode = 0x01;
                  }else if(this.mouseselectvalue==6){//down
                    result.mediaExtraCode = 0x02;
                  }else if(this.mouseselectvalue==7){//left
                    result.mediaExtraCode = 0x03;
                  }else if(this.mouseselectvalue==8){//right
                    result.mediaExtraCode = 0x04;
                  }
                  result.keyCode = this.mouseinput + 31;
             }else if(this.mouseselectvalue==9 || this.mouseselectvalue==10){ //move scrool
                  result.funcCode= this.mousecheck?0x09:0x08;
                  if(this.mouseselectvalue==9){//up
                    result.mediaExtraCode = 0x01;
                  }else if(this.mouseselectvalue==10){//down
                    result.mediaExtraCode = 0x02;
                  }
                  if(this.mousescrool==1){
                    result.keyCode = 1;
                  }else if(this.mousescrool==2){
                    result.keyCode = 5;
                  }else if(this.mousescrool==3){
                    result.keyCode = 20;
                  }else if(this.mousescrool==4){
                    result.keyCode = 60;
                  }else if(this.mousescrool==5){
                    result.keyCode = 100;
                  }
   
              }
            break;
          default://none
          this.$message.error(this.labelstr+this.$t('keyConfig.setErr'));
          return;
      }
          return result;
        },
        fucChange(){
      switch(this.keyradio){
        case 1://normal
          this.input1show = true;
          this.funcselectshow = false;
          this.mediaselectshow = false;
          this.input2show = false;
          this.mixshow = false;
          this.mouseselectshow = false;
          this.mouseshow=false;
          break;
        case 2://function
          this.input1show = false;
          this.funcselectshow = true;
          this.mediaselectshow = false;
          this.input2show = false;
          this.mixshow = false;
          this.mouseselectshow = false;
          this.mouseshow=false;
          this.funcoptions.splice(0,this.funcoptions.length);
          for(var i=0;i<this.functionkeys.length;i++){
            this.funcoptions.push({value:this.functionkeyvalues[i],label:this.functionkeys[i]});
          }
          break;
        case 3://media
          this.input1show = false;
          this.funcselectshow = false;
          this.mediaselectshow = true;
          this.input2show = false;
          this.mixshow = false;
          this.mouseselectshow = false;
          this.mouseshow=false;
          this.mediaoptions.splice(0,this.mediaoptions.length);
          for(var i=0;i<this.mediakeys.length;i++){
            this.mediaoptions.push({value:this.mediakeyvalues[i],label:this.mediakeys[i]});
          }
          break;
        case 4://mix
          this.input1show = false;
          this.funcselectshow = false;
          this.mediaselectshow = false;
          this.input2show = false;
          this.mixshow = true;
          this.mouseselectshow = false;
          this.mouseshow=false;
          this.mixfuncoptions.splice(0,this.mixfuncoptions.length);
          for(var i=0;i<this.functionkeys.length;i++){
            this.mixfuncoptions.push({value:this.functionkeyvalues[i],label:this.functionkeys[i]});
          }
          this.mixcountchange();
          break;
        case 5://string
          this.input1show = false;
          this.funcselectshow = false;
          this.mediaselectshow = false;
          this.input2show = true;
          this.mixshow = false;
          this.mouseselectshow = false;
          this.mouseshow=false;
          break;
        case 6://mouse
          this.input1show = false;
          this.funcselectshow = false;
          this.mediaselectshow = false;
          this.input2show = false;
          this.mixshow = false;
          this.mouseselectshow = true;
          this.mouseshow=true;
          this.mouseoptions.splice(0,this.mouseoptions.length);
          for(var i=0;i<this.mouses.length;i++){
            this.mouseoptions.push({value:(i+1),label:this.mouses[i]});
          }
          this.mousecheckchange();
          break;
        default://none
          this.input1show = false;
          this.funcselectshow = false;
          this.mediaselectshow = false;
          this.input2show = false;
          this.mixshow = false;
          this.mouseselectshow = false;
          this.mouseshow=false;
          break;
      }
 
    },
    mixdelete(){
      if(this.mixcount<=0){
        this.$message.error(this.$t('keyConfig.delErr'));
        return;
      }
      this.mixcount--;
      this.mixfuncselectvalue[this.mixcount]='';
      this.mixtext[this.mixcount]='';
      this.mixcountchange();
    },
    mixadd(type){
      if(this.mixcount>=5){
        this.$message.error(this.$t('keyConfig.mixMaxErr'));
        return;
      }        
      if(type==1){
        this.mixshowarray[this.mixcount]=true;
      }else if(type==2){
        this.mixshowarray[this.mixcount]=false;
      }
      this.mixcount++;   
      this.mixcountchange();
    },
    mixreset(){
      this.mixcount=0;
      this.mixfuncselectvalue=['','','','',''];
      this.mixtext=['','','','',''];
      this.mixshowarray=[false,false,false,false,false];
      this.mixcountchange();
    },
    mixcountchange(){
      switch(this.mixcount){
        case 0:
          this.mixcountarray=[false,false,false,false,false];
          this.mixtips=this.$t('keyConfig.mixAdd');
          break;         
        case 1:
        this.mixcountarray=[true,false,false,false,false];
          this.mixtips=this.$t('keyConfig.mixAdd');
          break;
        case 2:
          this.mixcountarray=[true,true,false,false,false];
          this.mixtips=this.$t('keyConfig.mixMax5');
          break;
        case 3:
          this.mixcountarray=[true,true,true,false,false];
          this.mixtips=this.$t('keyConfig.mixMax5');
          break;
        case 4:
          this.mixcountarray=[true,true,true,true,false];
          this.mixtips=this.$t('keyConfig.mixMax5');
          break;
        case 5:
          this.mixcountarray=[true,true,true,true,true];
          this.mixtips=this.$t('keyConfig.mixMax5');
          break;
        default:
          this.mixcountarray=[false,false,false,false,false];
          this.mixtips=this.$t('keyConfig.mixAdd');
          break;
      }
    }, mousecheckchange(){
      this.mousecheckshow = false;
      this.mouseinputshow = false;
      this.mousescroolshow = false;
      this.mousechecktxt="";
      this.mouseinputtxt="";
        if(this.mouseselectvalue==1 || this.mouseselectvalue==2 || this.mouseselectvalue==3 ){ //click
          this.mousecheckshow = true;
          this.mousechecktxt = this.$t('keyConfig.holdTip');
        }else  if(this.mouseselectvalue==5 || this.mouseselectvalue==6 || this.mouseselectvalue==7 || this.mouseselectvalue==8 ){ //move
          this.mousecheckshow = true;
          this.mouseinputshow = true;
          this.mousechecktxt = this.$t('keyConfig.holdTip');
          this.mouseinputtxt = this.$t('keyConfig.move');
        }else if(this.mouseselectvalue==9 || this.mouseselectvalue==10 ){ //scrool
          this.mousecheckshow = true;
          this.mousescroolshow = true;
          this.mousechecktxt = this.$t('keyConfig.holdTip');
          this.mouseinputtxt = this.$t('keyConfig.strong');
        }
    },
    refreshText(){
      this.mouses=this.$t('keyConfig.mouses');
      this.functionkeys=this.$t('keyConfig.functionkeys');
      this.mediakeys=this.$t('keyConfig.mediakeys');
      this.fucChange();
    }
}
}
</script>
<style scoped>
.el-row {
    margin-bottom: 20px;
  }
</style>
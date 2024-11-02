export default {
    index:{
        helpTitle:'Help',
        helpTips0:'Introduction of 5 key modes:',
        helpTips1:'1.Common key:Define a key of the 3-key keyboard as a common key that can be input, such as 1, 2, 3, a, b, c, ?, space, etc.',
        helpTips1_1:'Note: If you input uppercase letters or !@#$% keys, it is actually triggered by Shift+corresponding keys!',
        helpTips2:'2.Function key:Define a key on the 3-key keyboard as a function key such as Ctrl, Alt, Win, Command, and Enter',
        helpTips2_1:'F13-F24 are special function keys, which are not available on ordinary keyboards, and can be used as custom shortcut keys in the software to prevent conflicts!',
        helpTips3:'3.Media key:Define a key on the 3-key keyboard as a media key such as volume up, brightness up, and open browser',
        helpTips4:'4.Combination key:Define a key on a 3-key keyboard as a combination of 2-5 keys, such as one-key Ctrl+c, one-key Win+l, one-key Ctrl+Alt+Delete, etc.',
        helpTips5:'5.String:Define a button of the 3-key keyboard as an input string, such as one-key input user name, one-key input select * from, etc.',
        helpTips6:'6.Mouse:Define a button of the 3-key keyboard as mouse button click, mouse movement, scroll wheel, etc.',
        helpButton:'Help',
        connButtonText1:'Connect 3 Key keyboard',
        keyAllLabel:'When the 3 keys are pressed at the same time, they act as an independent keys',
        keyAllTrue:'On',
        keyAllFalse:'Off',
        changeLog4:'2024.7.12 Updated to threekey_v6.0.hex Added press enter after string input',
        changeLog3:'2023.6.16 Updated to threekey_v5.1.hex fix key2 string mode bug and some settings cannot saved bug',
        changeLog1:'2023.5.4 Updated to threekey_v5.0.hex, add setting it as a mouse',
        changeLog1_1:'How to update',
        changeLog2:'2023.4.25 Updated to threekey_v4.1.hex, which fixes the problem that when Windows presses the key immediately after connecting the keyboard, the key does not work',
        changeLog2_1:'How to update',
        videoText:'Video',
        keyTest:'Online Keyboard Testing Sites',
        mouseTest:'Online Mouse Testing Sites',
        openSource:'All software and hardware of the project are open source',
        backToIndex:'Back to topyuan.top',
        submit:'Submit',
        browserErr:'Please use Chrome or Edge browser version 103 or above to access this page via https!',
        firmwareErr:'The currently connected 3-key keyboard is not the latest firmware and does not support mouse settings, please update!!<br/><br/>New version:<br/>5.0 Added mouse<br/>4.1 fix bugs<br/>4.0 Added a combination of up to 5 keys, press three keys at the same time to one new key<br/>',
        firmwareErrv5:'The currently connected 3-key keyboard is not the latest firmware and does not support press enter after string input settings, please update!!<br/><br/>New version:<br/>6.0 Added press enter after string input<br/>',
        firmwareErrv6:'The currently connected 3-key keyboard is not the latest firmware and does not support press mouse when long press, please update!!<br/><br/>New version:<br/>7.0 Added press mouse when long press<br/>',
        tip:'Notice',
        updateHelp:'Update tutorial',
        updateNo:'Not updated yet, return',
        redirecting:'Redirecting...please wait',
        connFirst:'Please connect the keyboard first',
        disconnect:'Disconnect',
        success:'Setting successful!',
        firmwareErr1:'The currently connected 3-key keyboard is not the latest firmware, please update!!<br/><br/>New version:<br/>5.0 Added mouse<br/>4.1 fix bugs<br/>4.0 Added a combination of up to 5 keys, press three keys at the same time to one new key<br/>',
        updateNo1:'Not updated for now, change the key first (redirect to the old version key change page)',
        conError1:'The port is occupied, the connection failed!',
        conError2:'Connection failed, reason:',
        conError3:'Disconnect failed! Please refresh the page!',
        howtoaddress:'https://topyuan.top/3key/howtoupdate_en',
    },
    keyConfig:{
        keyDes1:'Common key',
        keyDes2:'Function key',
        keyDes3:'Media key',
        keyDes4:'Combination key',
        keyDes5:'String',
        keyDes6:'Mouse',
        select:'Please choose',
        mix1:'Add a function key',
        mix2:'Add a common key',
        mix3:'Delete a combination key',
        mix4:'Reset the combination key',
        mix5:'Common combination keys',
        mixtip:'The following are common key combinations, which can be quickly set. If you don’t know what to set, you can check it out , I will update it irregularly.',
        mixcommon1:'Combination key',
        mixcommon2:'Description',
        mixcommon3:'Operate',
        set:'Set up',
        inputErr:'Input error',
        selectErr:'Select error',
        mixErr:'The combination key is at least 2 key combination!',
        mixErr1:'Combination key No.',
        mixErr2:' select error',
        mixErr3:' input error',
        textErr:'String cannot be empty',
        textErr1:'String input error, there is no such key',
        setErr:'Setting error',
        delErr:'Can no longer be deleted!',
        mixMaxErr:'Supports up to 5 key combinations!',
        mixAdd:'Please add function keys or common keys',
        mixMax5:'All the above key combinations, support up to 5 keys',
        holdTip:'Trigger continuously when long press',
        holdPressTip:'Press mouse when long press',
        stringEnter:'Press enter after input',
        move:'Moving distance',
        strong:'Intensity',
        mouses:['Left Click','Middle Click','Right Click','Random Move','Move Up','Move Down','Move Left','Move Right','Scroll Wheel Up','Scroll Wheel Down'],
        functionkeys:["Left Ctrl", "Left Shift", "Left Alt", "Left Win/Command", "Right Ctrl", "Right Shift", "Right Alt", "Right Win/Command", "Up", "Down", "Left", "Right", "Backspace", "Tab", "Enter", "ESC", "Insert", "Delete", "Page Up", "Page Down", "Home", "End", "Caps Lock", "Print Screen", "Scroll Lock", "Pause Break", "Num Lock", "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12", "F13", "F14", "F15", "F16", "F17", "F18", "F19", "F20", "F21", "F22", "F23", "F24", "Menu", "/(num pad)", "*(num pad)", "-(num pad)", "+(num pad)", "Enter(num pad)", "1(num pad)", "2(num pad)", "3(num pad)", "4(num pad)", "5(num pad)", "6(num pad)", "7(num pad)", "8(num pad)", "9(num pad)", "0(num pad)", ".(num pad)"],
        mediakeys:["Power", "Sleep", "Record", "Fast Forward", "Rewind", "Next", "Previous", "Stop", "Play/Pause", "Pause", "Volume Mute", "Volume Up", "Volume Down","Brightness Up","Brightness Down","Screen Saver","Open Music","Open Email","Open Calculator","Open Explorer","Open Browser","Browser Back","Browser forward","Browser Refresh","Browser bookmarks"]
    }
}
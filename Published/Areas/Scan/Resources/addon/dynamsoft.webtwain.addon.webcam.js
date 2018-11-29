/*!
* Dynamsoft JavaScript Library
* Product: Dynamsoft Web Twain
* Web Site: http://www.dynamsoft.com
*
* Copyright 2015, Dynamsoft Corporation 
* Author: Dynamsoft R&D Department
*
* Version: 10.2.0.324
*
* Module: htm5
* final js: build\addon\dynamsoft.webtwain.addon.webcam.js
*/
;Dynamsoft.WebcamVersion="10, 2, 0, 324";var EnumDWT_VideoProperty={VP_BRIGHTNESS:0,VP_CONTRAST:1,VP_HUE:2,VP_SATURATION:3,VP_SHARPNESS:4,VP_GAMMA:5,VP_COLORENABLE:6,VP_WHITEBALANCE:7,VP_BACKLIGHTCOMPENSATION:8,VP_GAIN:9};var EnumDWT_CameraControlProperty={CCP_PAN:0,CCP_TILT:1,CCP_ROLL:2,CCP_ZOOM:3,CCP_EXPOSURE:4,CCP_IRIS:5,CCP_FOCUS:6};(function(a){function b(){var c=this;c._errorCode=0;c._errorString="";c._Count=0;c._resultlist=[]}b.prototype.GetCount=function(){var c=this;return c._Count-1};b.prototype.Get=function(d){var e=this,c=e._resultlist.length;if(e._errorCode<0){return""}if(d>=c-1||d<0){Dynamsoft.Lib.Errors.Webcam_InvalidIndex(e,"Get");return""}return e._resultlist[d]};b.prototype.GetCurrent=function(){var d=this,c=d._resultlist.length;if(d._errorCode<0){return""}return d._resultlist[c-1]};a.NewWebcamValue=function(){return new b()}})(Dynamsoft.Lib);(function(a){function b(){var c=this;c._errorCode=0;c._errorString="";c._Count=0;c._resultlist=[]}b.prototype.GetValue=function(){var d=this,c=d._resultlist.length;if(d._errorCode<0){return""}return d._resultlist[0].current};b.prototype.GetIfAuto=function(){var d=this,c=d._resultlist.length;if(d._errorCode<0){return""}return d._resultlist[0].auto=="true"?true:false};a.NewWebcamSetting=function(){return new b()}})(Dynamsoft.Lib);(function(a){function b(){var c=this;c._errorCode=0;c._errorString="";c._Count=0;c._resultlist=[]}b.prototype.GetMinValue=function(){var d=this,c=d._resultlist.length;if(d._errorCode<0){return""}return d._resultlist[0].min};b.prototype.GetMaxValue=function(){var d=this,c=d._resultlist.length;if(d._errorCode<0){return""}return d._resultlist[0].max};b.prototype.GetSteppingDelta=function(){var d=this,c=d._resultlist.length;if(d._errorCode<0){return""}return d._resultlist[0].step};b.prototype.GetDefaultValue=function(){var d=this,c=d._resultlist.length;if(d._errorCode<0){return""}return d._resultlist[0].defaultValue};b.prototype.GetIfAuto=function(){var d=this,c=d._resultlist.length;if(d._errorCode<0){return""}return d._resultlist[0].auto=="true"?true:false};a.NewWebcamMoreSetting=function(){return new b()}})(Dynamsoft.Lib);(function(a){var b=function(c){var d=a.html5.Funs;c._innerSetWebcamValue=function(f){var g=this,e;e=a.NewWebcamValue();e._errorCode=g._errorCode;e._errorString=g._errorString;if(f&&a.isArray(f)){e._resultlist=f;e._Count=f.length}return e};c._innerSetWebcamSetting=function(f){var g=this,e;e=a.NewWebcamSetting();e._errorCode=g._errorCode;e._errorString=g._errorString;if(f&&a.isArray(f)){e._resultlist=f;e._Count=f.length}return e};c._innerSetWebcamMoreSetting=function(f){var g=this,e;e=a.NewWebcamMoreSetting();e._errorCode=g._errorCode;e._errorString=g._errorString;if(f&&a.isArray(f)){e._resultlist=f;e._Count=f.length}return e}};a.DynamicLoadAddonFuns.push(b);a.Addon_Events.push("OnCaptureStart");a.Addon_Events.push("OnCaptureSuccess");a.Addon_Events.push("OnCaptureError");a.Addon_Events.push("OnCaptureEnd");a.Addon_Sendback_Events.push("OnCaptureStart");a.Addon_Sendback_Events.push("OnCaptureSuccess");a.Addon_Sendback_Events.push("OnCaptureError")})(Dynamsoft.Lib);(function(a){if(!a.product.bChromeEdition){return}function b(d){var e=a.html5.Funs;d._innerWebcamValueFunction=function(f,h){var i=this,g;g=i._innerFunRaw(f,h,false,false);return i._innerSetWebcamValue(g)};d._innerWebcamSettingFunction=function(f,h){var i=this,g;g=i._innerFunRaw(f,h,false,false);return i._innerSetWebcamSetting(g)};d._innerWebcamMoreSettingFunction=function(f,h){var i=this,g;g=i._innerFunRaw(f,h,false,false);return i._innerSetWebcamMoreSetting(g)};d._innerCaptureImageFunction=function(h,f,k,l,g,o){var j=this;var i=function(m){e.hideMask(h)},n=function(m){e.hideMask(h)};e.showMask(h);j.__OnCaptureStart=k;j.__OnCaptureSuccess=l;j.__OnCaptureError=g;j.__OnCaptureEnd=o;j._innerSend(h,f,true,i,n);return true};d._OnCaptureStart=function(f){var g=this;console.log("webcam event: OnCaptureStart");if(Dynamsoft.Lib.isFunction(g.__OnCaptureStart)){g.__OnCaptureStart()}};d._OnCaptureSuccess=function(f){var g=this;console.log("webcam event: OnCaptureSuccess");if(Dynamsoft.Lib.isFunction(g.__OnCaptureSuccess)){g.__OnCaptureSuccess()}};d._OnCaptureError=function(g){var i=this,f=g[1],h=g[2];console.log("webcam event: OnCaptureError");if(Dynamsoft.Lib.isFunction(i.__OnCaptureError)){i.__OnCaptureError(f,h)}};d._OnCaptureEnd=function(f){var g=this;console.log("webcam event: OnCaptureEnd");if(Dynamsoft.Lib.isFunction(g.__OnCaptureEnd)){g.__OnCaptureEnd()}}}var c=function(e){var f=a.html5.Funs,d;b(e);d={Webcam:{Download:function(l,o,h){var k=e,m;Dynamsoft.Lib.cancelFrome=2;var j=function(){if(Dynamsoft.Lib.isFunction(o)){o()}return true},p=function(){if(Dynamsoft.Lib.isFunction(h)){h(k.ErrorCode,k.ErrorString)}return false};var g=e._innerFun("GetAddOnVersion",f.makeParams("webcam"));if(g==Dynamsoft.WebcamVersion){j();return true}if(!l||l==""){Dynamsoft.Lib.Errors.Webcam_InvalidRemoteFilename(k);p();return false}if(f.isServerInvalid(k)){p();return false}m="get";Dynamsoft.Lib.showProgress(k,"Download",true);var n=function(q){var r=(q.total===0)?100:Math.round(q.loaded*100/q.total),s=[q.loaded," / ",q.total].join("");k._OnPercentDone([0,r,"","http"])},i=true;k._OnPercentDone([0,-1,"HTTP Downloading...","http"]);if(!Dynamsoft.Lib.isFunction(o)){i=false}f.loadHttpBlob(k,m,l,i,function(q){k._OnPercentDone([0,-1,"Loading..."]);var r=100;k.__LoadImageFromBytes(q,r,"",i,j,p)},function(){Dynamsoft.Lib.closeProgress("Download");p()},n)},GetSourceList:function(){var g=e._innerFunRaw("GetWebcamSourceList");g.splice(g.length-1,1);return g},SelectSource:function(g){return e._innerFun("SelectWebcam",f.makeParams(g))},CloseSource:function(){return e._innerFun("StopCapture")},CaptureImage:function(g,j,i,h,k){e._innerCaptureImageFunction("CaptureImage",f.makeParams(g),j,i,h,k)},GetMediaType:function(){return e._innerWebcamValueFunction("GetMediaType")},SetMediaType:function(g){return e._innerFun("SetMediaType",f.makeParams(g))},GetResolution:function(){return e._innerWebcamValueFunction("GetResolution")},SetResolution:function(g){return e._innerFun("SetResolution",f.makeParams(g))},GetFrameRate:function(){return e._innerWebcamValueFunction("GetFrameRate")},SetFrameRate:function(g){return e._innerFun("SetFrameRate",f.makeParams(g))},GetVideoPropertySetting:function(g){return e._innerWebcamSettingFunction("GetWebcamVideoPropertySetting",f.makeParams(g))},GetVideoPropertyMoreSetting:function(g){return e._innerWebcamMoreSettingFunction("GetWebcamVideoPropertyMoreSetting",f.makeParams(g))},SetVideoPropertySetting:function(h,g,i){return e._innerFun("SetWebcamVideoPropertySetting",f.makeParams(h,g,i))},GetCameraControlPropertySetting:function(g){return e._innerWebcamSettingFunction("GetWebcamCameraControlSetting",f.makeParams(g))},GetCameraControlPropertyMoreSetting:function(g){return e._innerWebcamMoreSettingFunction("GetWebcamCameraControlMoreSetting",f.makeParams(g))},SetCameraControlPropertySetting:function(h,g,i){return e._innerFun("SetWebcamCameraControlPropertySetting",f.makeParams(h,g,i))}}};e.__addon=e.__addon||{};a.mix(e.__addon,d)};a.DynamicLoadAddonFuns.push(c)})(Dynamsoft.Lib);(function(a){if(!a.product.bPluginEdition&&!a.product.bActiveXEdition){return}function b(d){d._innerRead=function(f){var h=this,e;try{e=KISSY.JSON.parse(f)}catch(g){}return e.result};d._innerWebcamValueFunctionPlugin=function(f){var g=this,e;e=g._innerRead(f);return g._innerSetWebcamValue(e)};d._innerWebcamSettingFunctionPlugin=function(f){var g=this,e;e=g._innerRead(f);return g._innerSetWebcamSetting(e)};d._innerWebcamMoreSettingFunctionPlugin=function(f){var g=this,e;e=g._innerRead(f);return g._innerSetWebcamMoreSetting(e)}}var c=function(f){var e=false,g,d;b(f);if(f.getSWebTwain()&&f.getSWebTwain().Addon){e=true}if(!e){return false}g=f.getSWebTwain();d={Webcam:{Download:function(l,h,j){var k=g.GetAddOnVersion("webcam");if(k==Dynamsoft.WebcamVersion){if(Dynamsoft.Lib.isFunction(h)){h()}return true}var i=g.DownloadAddon(l);return a.wrapperRet(f,i,h,j)},GetSourceList:function(){var h=f._innerRead(g.GetWebcamSourceList());h.splice(h.length-1,1);return h},SelectSource:function(h){return g.SelectWebcam(h)},CloseSource:function(){return g.StopCapture()},CaptureImage:function(h,k,j,i,l){g.CaptureImage(h,k,j,i,l)},GetMediaType:function(){return f._innerWebcamValueFunctionPlugin(g.GetMediaType())},SetMediaType:function(h){return g.SetMediaType(h)},GetResolution:function(){return f._innerWebcamValueFunctionPlugin(g.GetResolution())},SetResolution:function(h){return g.SetResolution(h)},GetFrameRate:function(){return f._innerWebcamValueFunctionPlugin(g.GetFrameRate())},SetFrameRate:function(h){return g.SetFrameRate(h)},GetVideoPropertySetting:function(h){return f._innerWebcamSettingFunctionPlugin(g.GetWebcamVideoPropertySetting(h))},GetVideoPropertyMoreSetting:function(h){return f._innerWebcamMoreSettingFunctionPlugin(g.GetWebcamVideoPropertyMoreSetting(h))},SetVideoPropertySetting:function(i,h,j){return g.SetWebcamVideoPropertySetting(i,h,j)},GetCameraControlPropertySetting:function(h){return f._innerWebcamSettingFunctionPlugin(g.GetWebcamCameraControlSetting(h))},GetCameraControlPropertyMoreSetting:function(h){return f._innerWebcamMoreSettingFunctionPlugin(g.GetWebcamCameraControlMoreSetting(h))},SetCameraControlPropertySetting:function(i,h,j){return g.SetWebcamCameraControlPropertySetting(i,h,j)}}};f.Addon=f.Addon||{};a.mix(f.Addon,d)};a.DynamicLoadAddonFuns.push(c)})(Dynamsoft.Lib);

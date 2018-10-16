import '../util/util.js'
import { getCookie } from '../util/util.js';
import Axios from 'axios';
import '../assets/js/jquery.min.js'

//添加请求拦截器
Axios.interceptors.response.use(function(config){
  var zht=getCookie('admin_info_zht');
  var info=config.data;
  // let temp = info.toString();
  if (Object.prototype.toString.call(info) === "[object String]") {
    var temp=info.toString();
    if(temp.indexOf('top.location.href = location.href;') !==-1){
      //alert('登录身份过期，请重新登录');
      window.location.href='/';
      return;
    }
    return config;
  }else{
    return config;
  }
  return config;
},function(error){
  return Promise.reject(error);
});
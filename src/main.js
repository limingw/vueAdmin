// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import ElementUI from 'element-ui';
import 'element-ui/lib/theme-chalk/base.css';
import CollapseTransition from 'element-ui/lib/transitions/collapse-transition';
import axios from 'axios'
import 'element-ui/lib/theme-chalk/index.css';
import router from './router'
import store from './store'
import Datatable from 'vue2-datatable-component'

// 引入自定义的公共样式
import '../src/css/comview.less'

// 引入jQuery
import './assets/js/jquery.min.js'
import './js/unit'
import '../src/util/util.js'

import { getCookie } from '../src/util/util.js';

Vue.use(ElementUI);
Vue.component(CollapseTransition.name, CollapseTransition);
Vue.prototype.$http = axios;
Vue.config.productionTip = false;

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  store,  // 使用store
  components: { App },
  template: '<App/>'
});

//路由身份验证
router.beforeEach((to, from, next) => {
  if(to.path==='/'){
    next();
  }else{
    // var zht=getCookie('admin_info_zht');
    // var _this=this;
    // if(zht===null&&(to.path==='/Admin/Index'||to.path==='/Dashboard'||to.path==='/Admin/Index/RoleMg'||
    //   to.path==='/Admin/Index/Customs'||to.path==='/Admin/Index/AdsMg'||to.path==='/Admin/Index/OpsMg'||
    //   to.path==='/Admin/Index/AgentsMg'||to.path==='/Admin/Index/Finance'||to.path==='/Admin/Index/InavAd'||
    //   to.path==='/Admin/Index/AnLI'||to.path==='/Admin/Index/NodeMonitor'||to.path==='/Admin/Index/FlowsMg'||
    //   to.path==='/Admin/Index/SalesMg'||to.path==='/Admin/Index/OthersMg'||to.path==='/Admin/Index/WxRedPack'
    // )){
    //   alert('登录身份过期，请重新登录');
    //   next({path:'/'});
    // }else{
    //   next();
    // }
    next();
  }
  //next();
});

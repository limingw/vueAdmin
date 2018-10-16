// tab的vuex
import unit from "../../js/unit";
import '../../util/util.js'

const state = {
  options: [
    {
      title: '首页',
      link: '/Dashboard',
    }
  ],
  // 选中的tab
  tabActive: '/Dashboard',
  activeItem:'1',      //选中的菜单
  username:'',
}
const mutations = {
  // 添加导航栏
  AddLinks: function (state, obj) {
    let len = state.options.length;
    let bool = unit.GetListBool(state.options, obj.title);
    if (bool === true) {
      state.options.push(obj);
    }
  },
  // 删除导航栏
  RemoveLinks: function (state, title) {
    state.options.map((item, index) => {
      if (item.link === title) {
        state.options.splice(index, 1);
        return;
      }
    })
 },
  // 改变tab选中的路由
  TabActiveChange: function (state, url) {
    state.tabActive = url;
  },
  Activeitem:function (state,type){
    state.activeItem=type;
  },
  //登录用户
  users:function(state,username){
    //更新登录用户
    state.username=username;
  }
}

const actions = {

}

export default {
  state,
  mutations,
  actions

}


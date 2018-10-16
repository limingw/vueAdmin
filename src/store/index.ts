import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

// 导入状态
import links from './modules/links'


export default new Vuex.Store({
  modules: {
    links
  }
})


import Vue from 'vue'
import Router from 'vue-router'
import Login from '@/components/login'      //登录
import Index from '@/components/Index'      // 后台首页
import Dashboard from '@/views/DashBoard'   //首页
import RoleMg from '@/views/RoleMg'         //角色管理
import Customs from '@/views/Customs'       //客户管理
import AdsMg from '@/views/AdsMg'           //广告审核
import OpsMg from '@/views/OpsMg'           //运营管理
import AgentsMg from '@/views/AgentsMg'     //代理商管理
import Finance from '@/views/Finance'       //财务管理
import InavAd from '@/views/InavAd'         //互动类广告
import AnLI from '@/views/AnLI'             //案例管理
import NodeMonitor from '@/views/NodeMonitor'//节点监控
import FlowsMg from '@/views/FlowsMg'        //流量方管理
import SalesMg from '@/views/SalesMg'        //销售管理
import OthersMg from '@/views/OthersMg'      //系统设置
import WxRedPack from '@/views/WxRedPack'      //微信红包
import AgentFinance from '@/views/AgentFinance'      //微信红包

Vue.use(Router);

export default new Router({
  // mode:'history',
  routes: [
    {      //登录
      path:'/',
      name:'Login',
      component:Login
    },
    {
      path: '/Index',
      name: 'Index',
      component: Index,
      children: [    //子路由
        {             //首页
          path: '/Dashboard',
          name: 'Dashboard',
          component: Dashboard
        },
        {             //角色管理
          path: '/RoleMg',
          name: 'RoleMg',
          component: RoleMg,
        },
        {             //客户管理
          path: '/Customs',
          name: 'Customs',
          component: Customs,
        },
        {             //广告审核
          path: '/AdsMg',
          name: 'AdsMg',
          component: AdsMg,
        },
        {            //运营管理
          path: '/OpsMg',
          name: 'OpsMg',
          component: OpsMg,
        },
        {             //代理商管理
          path: '/AgentsMg',
          name: 'AgentsMg',
          component: AgentsMg,
        },
        {             //财务管理
          path: '/Finance',
          name: 'Finance',
          component: Finance
        },
        {             //互动类广告
          path: '/InavAd',
          name: 'InavAd',
          component: InavAd
        },
        {             //案例管理
          path: '/AnLI',
          name: 'AnLI',
          component: AnLI
        },
        {            //节点监控
          path: '/NodeMonitor',
          name: 'NodeMonitor',
          component: NodeMonitor
        },
        {            //流量方管理
          path: '/FlowsMg',
          name: 'FlowsMg',
          component: FlowsMg
        },
        {            //销售管理
          path: '/SalesMg',
          name: 'SalesMg',
          component: SalesMg
        },
        {              //系统设置
          path: '/OthersMg',
          name: 'OthersMg',
          component: OthersMg
        },
        {              //微信红包
          path: '/WxRedPack',
          name: 'WxRedPack',
          component: WxRedPack
        },
        {              //微信红包
          path: '/AgentFinance',
          name: 'AgentFinance',
          component: AgentFinance
        }
      ]
    },

  ]
})

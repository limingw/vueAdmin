<template>
  <div class="index">
    <el-container>
  		<el-aside>
				<!-- 左边侧边栏 -->
    		<leftNav></leftNav>
			</el-aside>
			<el-container>
				<el-header>
					<!-- 顶部用户信息栏 -->
    			<topNav></topNav>
				</el-header>
				<el-main>
					<!-- 引入页面区域 -->
    			<div class="main">
        		<el-tabs v-model="tabActive" type="card" closable @tab-remove="RemoveLinks" @tab-click="GoLinks">
						<el-tab-pane
								v-for="(item, index) in links"
								:key="item.link"
								:label="item.title"
								:name="item.link"
								:data-url = "item.link"
								style="margin:10px 0px 0px 30px;">
							
						</el-tab-pane>
       		 	</el-tabs>
              <div class="main1">
                  <router-view></router-view>
              </div>
    			</div>
				</el-main>

				<el-footer>
					<!--尾部-->
					<div class="footer">
						Copyright 2011 - 2018 All Rights Reserved 深圳市麦斯芬奇广告传媒公司版权所有 <a href="http://www.miitbeian.gov.cn/" target="_blank">粤ICP备17021406号-1</a>
					</div>
				</el-footer>
  		</el-container>
		</el-container> 
  </div>
</template>

<script type="text/javascript">
  import topNav from '@/components/topNav/'
  import leftNav from '@/components/leftNav/'

  import { mapState } from 'vuex'
  import '../js/axios.js'
  import '../css/common.less'
  import '../css/index.less'
import { getCookie } from '../util/util.js';
  

  export default {
    name: 'Index',
    data() {
      return {
        tabIndex: 1,
        bool: true,
        coll:false,
      }
    },
    components:{
      topNav,
      leftNav
    },
    computed: {
      ...mapState({
        links: state => state.links.options
      }),
      tabActive: {
        get () {
          return this.$store.state.links.tabActive
        },
        set (value) {
          this.$store.commit('TabActiveChange', value);
        }

      }
    },
    methods: {
      AddLinks: function (name, title,isCollapse) {
        this.$store.commit('AddLinks', {name, title});
        if(isCollapse){
          $('.el-aside').css('width','63px');
        }else{
          $('.el-aside').css('width','200px');
        }
      },
      GoLinks: function() {                 //点击tab
        this.$router.push({path: this.tabActive});
        this.$store.commit('TabActiveChange', this.tabActive);
        // let activeType='1';
        // switch (this.tabActive) {
        //   case '/DashBoard':
        //     activeType='1';
        //   break;
        //   case '/RoleMg':
        //     activeType='2';
        //   break;
        //   case '/Customs':
        //     activeType='3';
        //   break;
        //   case '/AdsMg':
        //     activeType='4';
        //   break;
        //   case '/OpsMg':
        //     activeType='5';
        //   break;
        //   case '/AgentsMg':
        //     activeType='6';
        //   break;
        //   case '/Finance':
        //     activeType='7';
        //   break;
        //   case '/AgentFinance':
        //     activeType='8';
        //   break;
        //   case '/InavAd':
        //     activeType='9';
        //   break;
        //   case '/AnLi':
        //     activeType='10';
        //   break;
        //   case '/NodeMonitor':
        //     activeType='11';
        //   break;
        //   case '/FlowsMg':
        //     activeType='12';
        //   break;
        //   case '/SalesMg':
        //     activeType='13';
        //   break;
        //   case '/OthersMg':
        //     activeType='14';
        //   break;
        //   case '/WxRedPack':
        //     activeType='15';
        //     break;
        //   default:
        //     break;
        // }
        // this.$store.commit('Activeitem',activeType);
      },
      RemoveLinks: function (targetName) {       //移除选中的tab
        let tabs = this.links;
        let activeName = this.tabActive;
        let activeType='1';
        var _this=this;
        if (activeName === targetName) {
          tabs.forEach((tab, index) => {
            if (tab.link === targetName) {
              let nextTab = tabs[index + 1] || tabs[index - 1];
              if (nextTab) {
                activeName = nextTab.link;
                this.$router.push({path: nextTab.link});
                // switch (activeName) {
                //   case '/DashBoard':
                //     activeType='1';
                //     break;
                //   case '/RoleMg':
                //     activeType='2';
                //     break;
                //   case '/Customs':
                //     activeType='3';
                //     break;
                //   case '/AdsMg':
                //     activeType='4';
                //     break;
                //   case '/OpsMg':
                //     activeType='5';
                //     break;
                //   case '/AgentsMg':
                //     activeType='6';
                //     break;
                //   case '/Finance':
                //     activeType='7';
                //     break;
                //   case '/AgentFinance':
                //     activeType='8';
                //     break;
                //   case '/InavAd':
                //     activeType='9';
                //     break;
                //   case '/AnLi':
                //     activeType='10';
                //     break;
                //   case '/NodeMonitor':
                //     activeType='11';
                //     break;
                //   case '/FlowsMg':
                //     activeType='12';
                //     break;
                //   case '/SalesMg':
                //     activeType='13';
                //     break;
                //   case '/OthersMg':
                //     activeType='14';
                //     break;
                //   case '/WxRedPack':
                //     activeType='15';
                //     break;
                //   default:
                //     break;
                // }
                // _this.$store.commit('Activeitem',activeType);
              }
            }
          });
        }
        this.$store.commit('TabActiveChange', activeName);
        this.$store.commit('RemoveLinks', targetName);
      },
    },
    created: function () {
      this.$router.push({path: '/Dashboard'});
      for (let index = 0; index < 10; index++) {
        $('#c_n'+index).css('display','none');
      }
      // var staffname=localStorage.getItem('staffname');
      // var role=localStorage.getItem('role');
      // if(staffname==null||staffname==''){        //验证身份
      //   this.$router.push({path: '/login'});
      //   return;
      // }else if(role==null||role==''){
      //   this.$router.push({path: '/login'});
      //   return;
      // }

      // var zht=getCookie('admin_info_zht');
      // if(zht==''||zht==null){
      //   this.$router.push({path: '/login'});
      //   return;
      // }
      
    },
    mounted(){
      let _this=this;
      if(_this.coll){
        $('.el-aside').css("width","63px");
      }else{
        $('.el-aside').css("width","200px");
      }
    }
  }

</script>

<style >
  .el-tabs__nav .el-tabs__item:first-child span{
    display: none!important;
  }
   
  .fade-enter {
    opacity:0;
  }
  .fade-leave{
    opacity:1;
  }
  .fade-enter-active{
     opacity:0;
    transition:opacity .1s;
  }
  .fade-leave-active{
    opacity:0;
    transition:opacity .5s;
  }
</style>

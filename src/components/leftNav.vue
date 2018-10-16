<template>
    <div class="left-nav">
        
         <div class="logodiv">
            <span class="iconzft">ZFT</span>
            <img src="../assets/images/logo1.png" class="float-left logo" />
        </div>
        
        <div class="menu">   
            <el-radio-group v-model="isCollapse">
            <el-radio-button v-if="isCollapse" :label="false" :click="zoom(isCollapse)"><i class="icon iconfont icon-liebiao liebiao"></i></el-radio-button>
            <el-radio-button v-else :label="true" :click="zoom(isCollapse)"><i class="icon iconfont icon-liebiao liebiao"></i></el-radio-button>
        </el-radio-group>
        <el-menu :default-active="activeItem" active-text-color="#ffffff" class="el-menu-vertical-demo" @open="handleOpen" @close="handleClose" :collapse="isCollapse" v-model="isCollapse">
            <router-link to="/DashBoard" v-if="dashboard">
                <el-menu-item index="1" @click="AddLinks('首页', '/DashBoard',isCollapse)">
                    <i class="icon iconfont icon-shouye"></i>
                    <span slot="title" class="leftcaidan">Dashboard</span>
                </el-menu-item>
            </router-link>

            <router-link to="/RoleMg" v-if="rolemg">
                <el-menu-item index="2" @click="AddLinks('角色管理', '/RoleMg',isCollapse)">
                    <i class="icon iconfont icon-jiaoseguanli"></i>
                    <span slot="title" class="leftcaidan">角色管理</span>
                </el-menu-item>
            </router-link>

            <router-link to="/Customs" v-if="customs">
                <el-menu-item index="3" @click="AddLinks('客户管理', '/Customs',isCollapse)">
                    <i class="icon iconfont icon-kehuguanli"></i>
                    <span slot="title" class="leftcaidan">客户管理</span>
                </el-menu-item>
            </router-link>

            <router-link to="/AdsMg" v-if="adsmg">
                <el-menu-item index="4" @click="AddLinks('广告审核', '/AdsMg',isCollapse)">
                    <i class="icon iconfont icon-guanggaoshenhe"></i>
                    <span slot="title" class="leftcaidan">广告审核</span>
                </el-menu-item>
            </router-link>

            <router-link to="/OpsMg" v-if="opsmg">
                <el-menu-item index="5" @click="AddLinks('运营管理', '/OpsMg',isCollapse)">
                    <i class="icon iconfont icon-yunyingguanli"></i>
                    <span slot="title" class="leftcaidan">运营管理</span>
                </el-menu-item>
            </router-link>

            <router-link to="/AgentsMg" v-if="agentsmg">
                <el-menu-item index="6" @click="AddLinks('代理商管理', '/AgentsMg',isCollapse)">
                    <i class="icon iconfont icon-01dailishangguanli"></i>
                    <span slot="title" class="leftcaidan">代理商管理</span>
                </el-menu-item>
            </router-link>

            <router-link to="/Finance" v-if="finance">
                <el-menu-item index="7" @click="AddLinks('财务管理', '/Finance',isCollapse)">
                    <i class="icon iconfont icon-caiwuguanli"></i>
                    <span slot="title" class="leftcaidan">财务管理</span>
                </el-menu-item> 
            </router-link>

            <router-link to="/AgentFinance" v-if="agentfinance">
                <el-menu-item index="8" @click="AddLinks('代理商财务', '/AgentFinance',isCollapse)">
                    <i class="icon iconfont icon-dailishangcaiwu"></i>
                    <span slot="title" class="leftcaidan">代理商财务</span>
                </el-menu-item> 
            </router-link>

            <router-link to="/InavAd" v-if="inavad">
                <el-menu-item index="9" @click="AddLinks('互动广告', '/InavAd',isCollapse)">
                    <i class="icon iconfont icon-hudongguanggao"></i>
                    <span slot="title" class="leftcaidan">互动广告</span>
                </el-menu-item>
            </router-link>

            <router-link to="/AnLi" v-if="anli">
                <el-menu-item index="10" @click="AddLinks('案例管理', '/AnLi',isCollapse)">
                    <i class="icon iconfont icon-anliguanli"></i>
                    <span slot="title" class="leftcaidan">案例管理</span>
                </el-menu-item>
            </router-link>
        
            <router-link to="/NodeMonitor" v-if="nodemonitor">
                <el-menu-item index="11" @click="AddLinks('节点监控', '/NodeMonitor',isCollapse)">
                    <i class="icon iconfont icon-jiedian"></i>
                    <span slot="title" class="leftcaidan">节点监控</span>
                </el-menu-item>
            </router-link>

            <!-- <router-link to="/FlowsMg" v-if="flowsmg">
                <el-menu-item index="12" @click="AddLinks('流量方管理', '/FlowsMg',isCollapse)">
                    <i class="icon iconfont icon-liuliang"></i>
                    <span slot="title" class="leftcaidan">流量方管理</span>
                </el-menu-item>
            </router-link>

            <router-link to="/SalesMg" v-if='salesmg' >
                <el-menu-item index="13" @click="AddLinks('销售管理', '/SalesMg',isCollapse)">
                    <i class="icon iconfont icon-xiaoshouguanli"></i>
                    <span slot="title" class="leftcaidan">销售管理</span>
                </el-menu-item>
            </router-link>

            <router-link to="/OthersMg" v-if='othersmg'>
                <el-menu-item index="14" @click="AddLinks('系统设置', '/OthersMg',isCollapse)">
                    <i class="icon iconfont icon-set"></i>
                    <span slot="title" class="leftcaidan">系统设置</span>  
                </el-menu-item>
            </router-link> -->

            <router-link to="/WxRedPack" v-if="wxredpack">
                <el-menu-item index="15" @click="AddLinks('微信红包', '/WxRedPack',isCollapse)">
                    <i class="icon iconfont icon-hongbao1"></i>
                    <span slot="title" class="leftcaidan">微信红包</span>
                </el-menu-item>
            </router-link>
        </el-menu>
        </div>
    </div>
</template>
<script>
  import { mapState } from 'vuex'
    import '../js/axios.js'
export default {
    name:'leftNav',
    data (){
        return {
            iszoom:true,
            isCollapse:false,
            tab:false,
            // activeitem:'1',              //选中的菜单栏
            dashboard:true,               //用于权限分配v-if
            rolemg:false,                   //角色管理
            customs:false,                   //客户管理
            adsmg:false,                     //广告审核
            opsmg:false,                     //运营管理
            agentsmg:false,                      //代理商管理
            agentfinance:false,                 //代理商财务
            finance:false,                       //财务管理
            inavad:false,                        //互动类广告
            anli:false,                          //案例管理
            nodemonitor:false,                   //节点管理
            flowsmg:false,                       //流量方管理
            salesmg:false,                       //销售管理
            othersmg:false,                      //其它设置
            wxredpack:false,                     //微信红包
        }
    },
    computed: {
      ...mapState({
        links: state => state.links.options,
        activeItem: state => state.links.activeItem
      }),
    },
    methods:{
        AddLinks: function (title, link,isCollapse) {
          this.$store.commit('AddLinks', {title, link});
          this.$store.commit('TabActiveChange', link);
        },
        handleOpen(key, keyPath) {
            console.log(key, keyPath);
        },
        handleClose(key, keyPath) {
            console.log(key, keyPath);
        },
        zoom(){
            if(this.isCollapse){
                $('.left-nav').animate({width:63},"slow");
                $('.el-aside').animate({width:63},"slow");
                // $('.left-nav li').css('padding-left','24px');
                $('.left-nav li').css('width','63px');
                $('.left-nav li:hover').css('cursor','pointer');
                $('.left-nav li:hover').css('font-size','28px');
                $('.logodiv').css('width','63px');
                $('.logodiv').css('background-color','#333333');
                $('.logo').css('display','none');
                $('.hide').css('display','none');
                $('.logo').css('display','none');
                $('.menu').css('display','none');
                setTimeout(function(){
                    $('.iconzft').css('display','block');
                    $('.hide').css('display','block');
                    $('.menu').css('display','block');
                },600);
                $('.iconzft').css('font-weight','600');
                $('.iconzft').css('font-size','20px');
                $('.iconzft').css('padding-top','30%');
                $('.liebiaoli').css('width','63px');
                $('.liebiao').css('padding-left','0px');
                $('.leftcaidan').css('display','none');
                $('.el-aside').css('width','63px');
            }else{
                $('.left-nav').animate({width:199},"slow");
                $('.el-aside').animate({width:199},"slow");
                $('.left-nav ul').css('width','200px');
                $('.left-nav li').css('height','45px');
                $('.left-nav li').css('text-align','left');
                $('.left-nav li').css('width','176px');
                $('.left-nav li').css('padding-left','24px');
                $('.left-nav li:hover').css('cursor','pointer');
                $('.logodiv').css('width','200px');
                $('.logodiv').css('background-color','#282828');
                $('.logo').css('display','block');
                $('.iconzft').css('display','none');
                $('.iconzft').css('font-weight','600');
                $('.iconzft').css('font-size','20px');
                $('.iconzft').css('padding-top','30%');
                $('.liebiaoli').css('width','200px');
                // $('.leftcaidan').css('display','block');
                //$('.leftcaidan').css('margin-left','3px');
            }
        }
    },
    created(){
        var _this=this;
        var roles=localStorage.getItem('role');
        if(roles!==''||roles!==null){
            switch (roles) {
                case '0':             //超级管理员
                    _this.dashboard=true;
                    _this.rolemg=true;
                    _this.customs=true;
                    _this.adsmg=true;
                    _this.opsmg=true;
                    _this.agentsmg=true;
                    _this.agentfinance=true;
                    _this.finance=true;
                    _this.inavad=true;
                    _this.anli=true;
                    _this.nodemonitor=true;
                    _this.flowsmg=true;
                    _this.salesmg=true;
                    _this.othersmg=true;
                    _this.wxredpack=true;
                    break;
                case '1':         //运营人员
                    _this.opsmg=true;
                    break;
                case '2':         //运营管理员
                    _this.opsmg=true;
                    break;
                case '3':         //代理商
                    _this.opsmg=true;
                    _this.customs=true;
                    _this.agentfinance=true;
                    break;
                case '4':             //销售
                    _this.salesmg=true;
                    break;
                case '5':             //广告审核人员
                    _this.adsmg=true;
                    break;  
                case '6':             //财务制表
                    _this.finance=true;
                    break;
                case '7':             //财务授权
                    _this.finance=true;
                    break;
                case '8':             //客户审核
                    _this.customs=true;
                    break;
                case '9':             //流量方审核
                    _this.flowsmg=true;
                    break;
                default:
                    break;
            }
        }else{
            alert('请先登录！');
            window.location.href='/#/';
        }
        
    },
}

</script>
<style scoped>

</style>

<template>
    <div class="top_nav">
        <el-dropdown @command="handleCommand" trigger="click">
            <span class="el-dropdown-link">
                {{user}}<i class="el-icon-arrow-down el-icon--right"></i>
            </span>
            <el-dropdown-menu slot="dropdown">
                <el-dropdown-item command="personal">个人资料</el-dropdown-item>
                <el-dropdown-item command="modify">密码修改</el-dropdown-item>
                <el-dropdown-item command="exit">安全退出</el-dropdown-item>
            </el-dropdown-menu>
        </el-dropdown>
    </div>
</template>
<script>
    import '../js/axios.js'
    import '../store/index.ts'
    export default {
        name:'topNav',
        data(){
            return{ 
                user:'',
            }
        },
        methods:{
             handleCommand(command){
                 var _this=this;
                 if(command=='personal'){
                    //window.location.href='/#/Index';
                 }else if(command=='modify'){
                    //window.location.href='/#/Index';
                 }else if(command=='exit'){
                    _this.$http.post('/Admin/Home/LoginOut').then(function(response){
                        _this.$message({
                            message:'退出成功，即将跳转到登录页面',
                            type:'success'
                        });
                    }).catch(function(error){
                       _this.$message({
                            message:'退出成功，即将跳转到登录页面',
                            type:'success'
                        });
                    });
                    setTimeout(function(){
                        _this.$router.push({path: '/login'});
                    },1500);
                 }
             }
        },
        created(){
            var _this=this;
            // _this.user=_this.$store.state.links.username;
            var staffname=localStorage.getItem('staffname');
            _this.user=staffname;
        },
    }
    
    import '../css/common.less'
import { Store } from 'vuex';
</script>
<style scoped>
    
</style>

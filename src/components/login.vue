<template>
    <div class="wrap_contents" v-loading="loading"
				element-loading-text="疯狂登录中"
				element-loading-spinner="el-icon-loading"
				element-loading-background="rgba(0, 0, 0, 0.8)">
        <div class="mainlogin">
            <div class="loginlogo">
                <img src="../assets/images/logo3.png" >
            </div>
            <div class="main-input">
				<div>
					<el-tag class="tags"><i class="icon iconfont icon-denglu"></i></el-tag>
					<el-input class="input" placeholder="请输入用户名" ref="focname" type="text" id="name" maxlength="50" v-model="name"></el-input>
				</div>

				<div>
					<el-tag class="tags"><i class="icon iconfont icon-mima"></i></el-tag>
					<el-input class="input" type="password" id="pass" ref="focpwd" placeholder="请输入密码" maxlength="30" v-model="password"></el-input>
				</div>

				<div>
					<el-tag class="tags"><i class="icon iconfont icon-yanzhengma"></i></el-tag>
					<el-input placeholder="验证码" type="text" class="codetxt" ref="foccode" id="codetxt" maxlength="4" v-model="code" @keyup.enter.native="login()"></el-input>
					<img v-bind:src="src" id="canvas" onclick="this.src=this.src+'?'+Math.random()">
				</div>
				<el-button class="btnlogin" @click="login()">登&nbsp;录</el-button>
            </div>
        </div>
    </div>
</template>

<script>
	import '../css/login.less'
    import '../assets/js/canvas-nest.min.js'
	import md5 from 'js-md5'
		
    export default {
		name: 'Login',
		data () {
            return {
                name:'',
                password:'',
                code:'',
                loading:false,
				denglu:false,
				src:''
            }
		},
		methods: {
			login(){      //登录
				let _this = this;
				if(this.name==''){
					this.$message({
						message: '请输入用户名',
						type: 'warning'
					});
					this.$refs.focname.focus();
				}else if(this.password==''){
					this.$message({
						message: '请输入密码',
						type: 'warning'
					});
					this.$refs.focpwd.focus();
				}else if(this.code==''){
					this.$message({
						message: '请输入验证码',
						type: 'warning'
					});
					this.$refs.foccode.focus();
				}else{
					setTimeout(function(){
								_this.$message({
                                    message: '登录成功,即将跳转首页',
                                    type: 'success'
                                });
							},800);
							setTimeout(function(){
								// location.href='/#/Admin/Index';
								_this.$router.push({path: '/Index'});
							},2000);
				}
			},
			// clearCanvas()
			// {  
			// 	for (let index = 0; index < 10; index++) {
			// 		var c=document.getElementById("c_n"+index);
					
			// 	}
				  
			// 	var cxt=c.getContext("2d");  
			// 	c.height=c.height;
			// }  
		},
		created(){
			// this.clearCanvas();
			for (let index = 0; index < 10; index++) {
				$('#c_n'+index).css('display','block');
				
			}
			this.$router.push({path: '/login'});
		},
		mounted(){
			var _this=this;
			_this.src='/Admin/Home/getvalidatecode?'+Math.random();
			this.$refs.focname.focus();
		},
    }
</script>
<style lang="less" scoped>

</style>

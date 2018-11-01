<template>
    <div class="welcome">
        <span class="member">欢迎{{user}},当前时间为：{{nowtime}}</span><hr>
    </div>
</template>

<script>
    import '../js/axios.js'
    export default {
        name:'DashBoard',
        data () {
            return {
                nowtime:new Date().toLocaleString(),
                user:'',
            }
        },
        mounted () {
			var _this=this;
			this.timer=setInterval(function(){
				_this.nowtime=new Date().toLocaleString();//修改date数据
			},1000);
		},
		beforeDestroy () {
			if(this.timer){
				clearInterval(this.timer);
			}
        },
        created(){
            // var _this=this;
            // var staffname=localStorage.getItem('staffname');
            // _this.user=staffname;
            // var role=localStorage.getItem('role');
            // if(staffname==null||staffname==''){
            //     alert('请先登录！');
            //     _this.$router.push({path: '/login'});
            //     return;
            // }else if(role==null||role==''){
            //     alert('请先登录！');
            //     window.location.href='/#/';
            //     _this.$router.push({path: '/login'});
            //     return;
            // }
        },
    }

    import '../css/dashbord.less'
</script>
<style scoped>

</style>

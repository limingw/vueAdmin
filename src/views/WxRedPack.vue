<template>
  <div class="redpack">
    <div class="redpacksum">
        <el-row class="todaysum">
            <el-col :span="12" class="elsum">
                <span>今日红包总量：{{index.todaysum}}</span>
            </el-col>

            <el-col :span="2" class="elimit" :offset="2">
                <el-tag type="info" class="tag">红包总个数</el-tag>
            </el-col>

            <el-col :span="3" class="elinput" :offset="1">
                <el-input v-model="index.txtlimit"
                    placeholder="30~100" ref="limit" clearable="">
                </el-input>
            </el-col>
            
            <el-col :span="2" :offset="1">
                <el-button type="primary" @click="modifyn()">修改</el-button>
            </el-col>
        </el-row>
        <el-row class="totalsum">
            <el-col :span="12">
                <span>微信红包总量：{{index.totalsum}}</span>
            </el-col>

            <el-col :span="2" class="elimit" :offset="2">
                <el-tag type="info" class="tag">红包总金额</el-tag>
            </el-col>

            <el-col :span="3" class="elinput" :offset="1">
                <el-input v-model="index.txtmoney"
                    placeholder="请输入红包总金额" ref="money" clearable="">
                </el-input>
            </el-col>
            
            <el-col :span="2" :offset="1">
                <el-button type="primary" @click="modifym()">修改</el-button>
            </el-col>
        </el-row>
    </div>

    <el-row class="rowtitle">
          <el-col :span="2" class="eltag">
            <el-tag type="info" class="tag">日期</el-tag>
          </el-col>
          <el-col :span="8" :offset="1" class="elinput">
            <el-date-picker
              v-model="index.time"
              type="daterange"
              align="left"
              unlink-panels
              range-separator="至"
              start-placeholder="开始日期"
              end-placeholder="结束日期"
              :picker-options="pickerOptions2"
              format="yyyy 年 MM 月 dd 日"
              value-format="yyyy-MM-dd">
            </el-date-picker>
          </el-col>
          <el-col :span="2" :offset="1">
            <el-button type="primary" @click="sertime()">查询</el-button>
          </el-col>
          <el-col :span="2" :offset="1">
            <el-button type="primary" @click="reset()">重置</el-button>
          </el-col>

        <el-col :span="3" class="elspan" :offset="2">
            <span>红包开关：</span>
        </el-col>

        <el-col :span="2" :offset="0" class="elswicth">
            <el-tooltip :content="'开关状态： ' + index.swicth" placement="bottom">
                <el-switch
                    v-model="index.swicth"
                    active-color="#13ce66"
                    inactive-color="#dcdfe6"
                    active-value="开"
                    inactive-value="关"
                    @change="chang()"
                    >
                </el-switch>
            </el-tooltip>
        </el-col>
    </el-row>

    <el-row class="redpacktable">
        <el-table border :data="index.tabledata"
            :default-sort="{prop:'RecordTime',order:'descending'}">
            <el-table-column
              header-align="center"
              align="center"
              prop="Openid"
              label="ID">
            </el-table-column>
            <el-table-column 
              header-align="center"
              align="center"
              prop="MchBillno"
              label="订单号">
            </el-table-column>
            <el-table-column
              header-align="center"
              align="center"
              prop="TotalAmount"
              label="红包金额">
            </el-table-column>
            <el-table-column 
              header-align="center"
              align="center"
              prop="RecordTime"
              sortable
              label="日期">
            </el-table-column>
          </el-table>
      </el-row>

      <el-row class="pagin" v-if="index.pagin">
        <el-pagination class="pagin"
            @size-change="handleSizeChange"
            @current-change="handleCurrentChange"
            :current-page.sync="index.currentPage"
            :page-sizes="[2, 4, 6, 8,10]"
            :page-size="index.pagesize"
            :pager-count="5"
            layout="total, sizes, prev, pager, next, jumper"
            :total="index.total">
          </el-pagination>
      </el-row>
  </div>
</template>

<script>
  import '../js/axios.js'
  import '../css/wxredpack.less'
    export default {
        name: "WxRedPack",
        data () {
            return {
                index:{
                    time:'',            //时间选择
                    swicth:'开',             //开关
                    todaysum:'0',              //今日红包总量
                    totalsum:'0',           //微信红包总量
                    txtlimit:'',              //红包个数输入框
                    txtmoney:'',              //红包总金额
                    tabledata:[],
                    currentPage:1,            //当前页
                    pagesize:8,              //每页大小
                    total:0,                    //总数
                    pagin:false,                    //分页v-if
                },
                pickerOptions2: {       //日期快捷选择
                    shortcuts: [{
                        text: '最近一周',
                        onClick(picker) {
                        const end = new Date();
                        const start = new Date();
                        start.setTime(start.getTime() - 3600 * 1000 * 24 * 7);
                        picker.$emit('pick', [start, end]);
                        }
                    }, {
                        text: '最近一个月',
                        onClick(picker) {
                        const end = new Date();
                        const start = new Date();
                        start.setTime(start.getTime() - 3600 * 1000 * 24 * 30);
                        picker.$emit('pick', [start, end]);
                        }
                    }, {
                        text: '最近三个月',
                        onClick(picker) {
                        const end = new Date();
                        const start = new Date();
                        start.setTime(start.getTime() - 3600 * 1000 * 24 * 90);
                        picker.$emit('pick', [start, end]);
                        }
                    }]
                },
            }
        },
        methods:{
            GetpackInfo(currentpage,pagesize,startime,endtime){         //查询列表
                var _this=this;
                var params={};
                if(startime==''||endtime==''){
                    params={
                        pageNo:_this.index.currentPage,
                        pageSize:_this.index.pagesize,
                    };
                }else{
                    params={
                        pageNo:_this.index.currentPage,
                        pageSize:_this.index.pagesize,
                        startTime:startime,
                        endTime:endtime
                    };
                }
                _this.index.currentPage=1;
                _this.$http.get('http://192.168.1.8:8077/WxCommon/GetAll',{
                    params:params
                }).then(function(response){
                    var info=response.data;
                    if(info!=''){
                        for (let index = 0; index < info.WxList.length; index++) {
                            _this.index.tabledata.push(info.WxList[index]);
                        }

                            if(info.RedPackStatus==''){
                                _this.index.swicth='开';
                            }else if(info.RedPackStatus=='on'){
                                _this.index.swicth='开';
                            }else if(info.RedPackStatus=='off'){
                                _this.index.swicth='关';
                            }else{
                                _this.index.swicth='开';
                            }

                            _this.index.limit=info.RedPackCount;
                            _this.index.total=info.TotaList;
                            _this.index.todaysum=info.TodayList;
                            _this.index.totalsum=info.TotaList;
                            _this.index.money=info.RedPackTotalAmount;
                            if(_this.index.total>_this.index.pagesize){
                                    _this.index.pagin=true;
                                }else{
                                    _this.index.pagin=false;
                                }
                        }else{
                            _this.index.limit='';
                            _this.index.total=0;
                            _this.index.todaysum=0;
                            _this.index.totalsum=0;
                            _this.index.money='';
                            _this.index.tabledata=[];
                        }
                }).catch(function(error){
                    _this.$message({
                        message:'服务器错误',
                        type:'error'
                    });
                });
            },
            sertime(){       //查询时间按钮
                var _this=this;
                _this.index.tabledata=[];
                if(this.index.time==''){
                    this.$message({
                        message: '请输入要查询的时间段',
                        type: 'warning'
                    });
                    return;
                }else{
                    _this.GetpackInfo(_this.index.currentPage,_this.index.pagesize,_this.index.time[0],_this.index.time[1]);
                }
            },
            reset(){
                var _this=this;
                _this.index.time='';
                _this.GetpackInfo(_this.index.currentPage,_this.index.pagesize,_this.index.time[0],_this.index.time[1]);
            },
            chang(callback){               //开关按钮状态改变
                var states="";
                var _this=this;
                if(this.index.swicth=='开'){
                    states='on';
                }else if(this.index.swicth=='关'){
                    states='off';
                }
                _this.$http.post('http://192.168.1.8:8077/WxCommon/ModifyStatus',{
                    status:states
                }).then(function(response){
                    if (states == 'off') {
                        if (response.data == 1001) {
                            _this.$message({
                                message: '关闭成功',
                                type: 'success'
                            });
                        } else {
                            _this.$message({
                                message: '关闭失败',
                                type: 'error'
                            });
                        }
                    } else {
                        if (response.data == 1001) {
                            _this.$message({
                                message: '开启成功',
                                type: 'success'
                            });
                        } else {
                            _this.$message({
                                message: '开启失败',
                                type: 'error'
                            });
                        }
                    }
                }).catch(function(error){
                    _this.$message({
                        message:'服务器错误',
                        type:'error'
                    });
                });
            },
            modifyn(){               //修改红包限额
                var _this=this;
                var reg = /^\+?[0-9][0-9]*$/;    //数字正则
                var num=parseInt(this.index.txtlimit);
                if(this.index.txtlimit==''){
                    this.$message({
                        message:'请输入红包限额',
                        type:'warning'
                    });
                    this.$refs.limit.focus();
                    return;
                }else if(!reg.test(this.index.txtlimit)){
                    this.$message({
                        message:'请输入正确的红包限额',
                        type:'warning'
                    });
                    this.$refs.limit.focus();
                    return;
                }else if(num<30||num>1000){
                    this.$message({
                        message:'红包限额在30~1000之间',
                        type:'warning'
                    });
                    this.$refs.limit.focus();
                    return;
                }else{
                    this.$http.post('http://192.168.1.8:8077/WxCommon/ModifyCount',{
                        count:num
                    }).then(function(response){
                        if(response.data==1001){
                            _this.$message({
                                message:'修改成功',
                                type:'success'
                            });
                        }else{
                            _this.$message({
                                message:'修改失败',
                                type:'error'
                            });
                        }
                    }).catch(function(error){
                        console.log(error);
                        _this.$message({
                            message:'服务器错误',
                            type:'error'
                        });
                    });
                }
            },
            modifym(){               //修改红包总金额
                var _this=this;
                var reg = /^\+?[0-9][0-9]*$/;    //数字正则
                var num=parseInt(this.index.txtmoney);
                if(this.index.txtmoney==''){
                    this.$message({
                        message:'请输入红包总金额',
                        type:'warning'
                    });
                    this.$refs.money.focus();
                    return;
                }else if(!reg.test(this.index.txtmoney)){
                    this.$message({
                        message:'请输入正确的红包总金额',
                        type:'warning'
                    });
                    this.$refs.money.focus();
                    return;
                }else if(num<0){
                    this.$message({
                        message:'红包总金额应该大于0',
                        type:'warning'
                    });
                    this.$refs.money.focus();
                    return;
                }else{
                    this.$http.post('http://192.168.1.8:8077/WxCommon/ModifyTotalAmount',{
                        count:num
                    }).then(function(response){
                        if(response.data==1001){
                            _this.$message({
                                message:'修改成功',
                                type:'success'
                            });
                        }else{
                            _this.$message({
                                message:'修改失败',
                                type:'error'
                            });
                        }
                    }).catch(function(error){
                        console.log(error);
                        _this.$message({
                            message:'服务器错误',
                            type:'error'
                        });
                    });
                }
            },
            handleSizeChange(val) {    //分页功能
                this.index.pagesize=val;
                var _this=this;
                _this.index.tabledata=[];
                _this.index.currentPage=1;
                _this.$http.get('http://192.168.1.8:8077/WxCommon/GetAll',{
                    params:{
                        pageNo:_this.index.currentPage,
                        pageSize:val
                    }
                }).then(function(response){
                    var info=response.data;
                    if(info!=''){
                        for (let index = 0; index < info.WxList.length; index++) {
                            _this.index.tabledata.push(info.WxList[index]);
                        }

                        if(info.RedPackStatus==''){
                            _this.index.swicth='开';
                        }else if(info.RedPackStatus=='on'){
                            _this.index.swicth='开';
                        }else if(info.RedPackStatus=='off'){
                            _this.index.swicth='关';
                        }else{
                             _this.index.swicth='开';
                        }

                        _this.index.limit=info.RedPackCount;
                        _this.index.total=info.TotaList;
                        _this.index.todaysum=info.TodayList;
                        _this.index.totalsum=info.TotaList;
                        _this.index.money=info.RedPackTotalAmount;
                        if(_this.index.total>_this.index.pagesize){
                            _this.index.pagin=true;
                        }else{
                            _this.index.pagin=false;
                        }
                    }else{
                        _this.index.limit='';
                        _this.index.total=0;
                        _this.index.todaysum=0;
                        _this.index.totalsum=0;
                        _this.index.money='';
                        _this.index.tabledata=[];
                    }
                }).catch(function(error){
                    _this.$message({
                        message:'服务器错误',
                        type:'error'
                    });
                });
            },
            handleCurrentChange(val){   //当前页改变
                var _this=this;
                _this.index.tabledata=[];
                _this.$http.get('http://192.168.1.8:8077/WxCommon/GetAll',{
                    params:{
                        pageNo:val,
                        pageSize:_this.index.pagesize
                    }
                }).then(function(response){
                    var info=response.data;
                    if(info!=''){
                        for (let index = 0; index < info.WxList.length; index++) {
                            _this.index.tabledata.push(info.WxList[index]);
                        }

                        if(info.RedPackStatus==''){
                            _this.index.swicth='开';
                        }else if(info.RedPackStatus=='on'){
                            _this.index.swicth='开';
                        }else if(info.RedPackStatus=='off'){
                            _this.index.swicth='关';
                        }else{
                             _this.index.swicth='开';
                        }

                        _this.index.limit=info.RedPackCount;
                        _this.index.total=info.TotaList;
                        _this.index.todaysum=info.TodayList;
                        _this.index.totalsum=info.TotaList;
                        _this.index.money=info.RedPackTotalAmount;
                        if(_this.index.total>_this.index.pagesize){
                            _this.index.pagin=true;
                        }else{
                            _this.index.pagin=false;
                        }
                    }else{
                        _this.index.limit='';
                        _this.index.total=0;
                        _this.index.todaysum=0;
                        _this.index.totalsum=0;
                        _this.index.money='';
                        _this.index.tabledata=[];
                    }
                }).catch(function(error){
                    _this.$message({
                        message:'服务器错误',
                        type:'error'
                    });
                });
            }
        },
        created(){
            var _this=this;

            //请求相应的token
            _this.$http.get('/Admin/WxRedPack/GetApiToken').then(function(response){
                localStorage.setItem('token', response.data.responseText);

                var token=localStorage.getItem('token');
                //带上token头部信息
                _this.$http.interceptors.request.use(config=>{
                    config.headers={
                        'ZFT_TOKEN':token,
                    }
                    return config;
                },err=>{
                    return Promise.reject(err);
                });

                _this.GetpackInfo(_this.index.currentPage,_this.index.pagesize,'','');
            }).catch(function(error){
                // localStorage.setItem('token', error.data.responseText);
                console.log(error);
            });
        },
    }
</script>

<style scoped>

</style>

<template>
  <div class="agents">
    <div v-show="index.index">
      <el-row class="search">
        <el-col :span="3" class="eltag">
          <el-tag type="info" class="tag">代理商名称：</el-tag>
        </el-col>
        <el-col :span="6" class="elinput" :offset="1">
          <el-input v-model="index.searchname" maxlength="50" ref="sername" class="search"
            placeholder="代理商名称"  @keyup.enter.native="sername()" clearable>
          </el-input> 
        </el-col>
        <el-col :span="2" :offset="1">
          <el-button type="primary" @click="sername()">查询</el-button>
        </el-col>
        <el-col :span="2" :offset="1">
          <el-button type="primary" @click="reset('name')">重置</el-button>
        </el-col>
      </el-row>

      <el-row class="table">
        <el-table stripe border
          :data="index.tabledata"
          :default-sort = "{prop: 'TotalRechargeSum', order: 'descending'}">
          <el-table-column
            header-align="center"
            align="center"
            prop="CompanyName"
            label="代理商名称">
          </el-table-column>
          <el-table-column
            header-align="center"
            align="center"
            prop="TotalRechargeSum"
            sortable
            label="总充值">
          </el-table-column>
          <el-table-column
            header-align="center"
            align="center"
            prop="TotalAssumeSum"
            sortable
            label="总消耗">
          </el-table-column>
          <el-table-column
            header-align="center"
            align="center"
            label="操作"
            width="200px">
            <template slot-scope="scope">
              <el-button type="primary" @click="details(scope.row)">详情</el-button>
              <el-tooltip class="item" effect="dark" content="此功能暂未开放！" placement="top">
                <el-button type="warning" @click="freeze(scope.row)" size="small">冻结</el-button>
              </el-tooltip>
            </template>
          </el-table-column>
        </el-table>
      </el-row>
    </div>

    <div v-show="index.detail" class="detail">
      <el-row class="info">
        <el-col :span="3" class="eltag">
          <el-tag type="info" class="tag">代理商名称</el-tag>
        </el-col>

        <el-col :span="7" class="elinput" :offset="1">
          <el-input v-model="detail.agname" class="search"
            readonly="readonly" clearable maxlength="50">
          </el-input> 
        </el-col>

        <el-col :span="2" class="reback">
          <el-button type="primary" @click="reback()">返回</el-button>
        </el-col>
      </el-row>

      <el-row class="info">
        <el-col :span="3" class="eltag">
           <el-tag type="info" class="tag">总充值</el-tag>
        </el-col>

        <el-col :span="7" class="elinput" :offset="1">
          <el-input v-model="detail.card" class="search"
            readonly="readonly" clearable>
          </el-input> 
        </el-col>
      </el-row>

      <el-row class="info">
        <el-col :span="3" class="eltag">
          <el-tag type="info" class="tag">总消耗</el-tag>
        </el-col>

        <el-col :span="7" class="elinput" :offset="1">
          <el-input v-model="detail.consume" class="search"
            readonly="readonly" clearable>
          </el-input> 
        </el-col>
      </el-row>

      <el-row class="searchtime">
        <el-col :span="2" class="eltag">
            <el-tag type="info" class="tag">注册日期：</el-tag>
        </el-col>
        <el-col :span="8" class="elinput" :offset="1">
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
              value-format="yyyy/MM/dd">
            </el-date-picker>
        </el-col>
        <el-col :span="2" :offset="1">
          <el-button type="primary" @click="sertime()">查询</el-button>
        </el-col>
        <el-col :span="2" :offset="1">
          <el-button type="primary" @click="reset('time')">重置</el-button>
        </el-col>
      </el-row>

      <el-row class="divtable">
         <!--表格部分-->
          <el-table stripe border
            :data="detail.tabledata"
            :default-sort = "{prop: 'TotalRechargeSum', order: 'descending'}">
            <el-table-column
              header-align="center"
              align="center"
              prop="CompanyName"
              label="公司名称">
            </el-table-column>
            <el-table-column
              header-align="center"
              align="center"
              prop="TotalRecharge"
              sortable
              label="总充值">
            </el-table-column>
            <el-table-column
              header-align="center"
              align="center"
              prop="TotalConsumed"
              sortable
              label="总消耗">
            </el-table-column>
            <el-table-column
              header-align="center"
              align="center"
              prop="CustomedAttach"
              label="所属运营"
              :formatter="opsmg">
            </el-table-column>
          </el-table>
      </el-row>
    </div>

    <div class="pagin" v-show="index.pagin">
      <el-row>
        <el-pagination class="pagin"
          @size-change="handleSizeChange"
          @current-change="handleCurrentChange"
          :current-page.sync="index.currentPage"
          :page-sizes="[2, 4, 6, 8]"
          :page-size="index.pagesize"
          :pager-count="5"
          layout="total, sizes, prev, pager, next, jumper"
          :total="index.total">
        </el-pagination>
      </el-row>
    </div>
  </div>

  
</template>

<script>
  import '../css/agentsmg.less'
  import '../js/axios.js'
  export default {
    name: "AgentsMg",
    data () {
      return {
        index:{     //主页
          searchname:'',      //搜索名
          time:'',            //搜索时间
          tabledata:[],        //表格数据
          index:true,           //主页v-show
          detail:false,          //详情v-show
          currentPage:1,            //当前页
          pagesize:8,              //每页大小
          total:1,                    //总数
          pagin:true,                    //分页v-if
        },
        detail:{      //详情页
          agname:'',          //代理商名称
          card:0,             //总充值
          consume:0,           //总消耗
          tabledata:[],        //表格数据
          agentid:'',
          currentPage:1,            //当前页
          pagesize:8,              //每页大小
          total:1,                    //总数
          pagin:true,                    //分页v-if
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
      paginhide(){                   //用于隐藏分页
        var _this=this;
        if(_this.index.total<=0){
          _this.index.pagin=false;
        }else{
          _this.index.pagin=true;
        };
      },
      GetAgentList(currentpage,pagesize,name){             //获取代理商列表
        var _this=this;
        var params={};
        if(name==''){
          params={
            pageNo:(currentpage-1)*pagesize,
            pageSize:pagesize
          };
        }else{
          params={
            pageNo:(currentpage-1)*pagesize,
            pageSize:pagesize,
            name:name
          };
        }
        _this.$http.get('/Admin/AgentsMg/VueGetAgentList',{
          params:params
        }).then(function(response){
          var info=response.data;
          if(info!=''||info!=null){
            _this.index.tabledata=info.AgencyDTOList;
            _this.index.total=info.TotalList;
          }else {
            _this.index.tabledata=[];
            _this.index.total=0;
          }
          _this.paginhide();
        }).catch(function(error){
          console.log(error);
          _this.$message({
            message:'服务器错误',
            type:'error'
          });
        });
      },
      GetCustomList(pagesize,currentpage,id,startime,endtime){   //获取代理商所属客户信息
        var _this=this;
        var params={};
        if(startime==''||endtime==''){
          params={
            pageSize:pagesize,
            pageNo:(currentpage-1)*pagesize,
            accountId:id
          };
        }else{
          params={
            pageSize:pagesize,
            pageNo:(currentpage-1)*pagesize,
            accountId:id,
            startTime:startime,
            endTime:endtime
          };
        }

        _this.$http.get('/Admin/AgentsMg/VueGetCustomList',{
          params:params
        }).then(function(response){
          var info=response.data;
          if(info!=''||info!=null){
            _this.detail.tabledata=info.CustomDTOList;
            _this.index.total=info.TotalList;
            var recharge=0;
            var consumed=0;
            if(info.CustomDTOList.length>=0){
              for (let index = 0; index < info.CustomDTOList.length; index++) {
                recharge+=parseFloat(info.CustomDTOList[index].TotalRecharge);
                consumed+=parseFloat(info.CustomDTOList[index].TotalConsumed);
              }
              _this.detail.card=recharge;
              _this.detail.consume=consumed;
            }else{
              _this.detail.card=recharge;
              _this.detail.consume=consumed;
            }
          }else{
            _this.detail.tabledata=[];
            _this.index.total=0;
            _this.detail.card=0;
            _this.detail.consume=0;
          }
          _this.paginhide();
        }).catch(function(error){
          console.log(error);
          _this.$message({
            message:'服务器错误',
            type:'error'
          });
        });
      },
      sername(){           //搜索
        var _this=this;
        if(this.index.searchname==''){
          this.$message({
            message:'请输入要查询的代理商名称',
            type:'warning'
          });
          this.$refs.sername.focus();
          return;
        }else{
          _this.GetAgentList(_this.index.currentPage,_this.index.pagesize,_this.index.searchname);
          _this.index.searchname='';
        }
      }, 
      sertime(){               //时间查询
        var _this=this;
        if(_this.index.time==''){
          _this.$message({
            message:'请先选择要查询的时间段',
            type:'warning'
          });
          return;
        }else{
          var startime=_this.index.time[0];
          startime=startime.replace('T',' ');
          startime=startime.split('.')[0];
          var endtime=_this.index.time[1];
          endtime=endtime.replace('T',' ');
          endtime=endtime.split('.')[0];
          _this.GetCustomList(_this.index.pagesize,_this.index.currentPage,_this.detail.agentid,startime,endtime);
          _this.index.time='';
        }
      },
      details(row){                   //详情
        var _this=this;
        _this.detail.agname=row.CompanyName;
        _this.detail.agentid=row.AccountId;
        _this.index.index=false;
        _this.index.detail=true;
        _this.GetCustomList(_this.index.pagesize,_this.index.currentPage,row.AccountId,'','');
      },
      freeze(row){            //冻结
          var _this=this;
          // this.$confirm('此操作将永久冻结该用户, 是否继续?', '提示', {
          //   confirmButtonText: '确定',
          //   cancelButtonText: '取消',
          //   type: 'warning'
          // }).then(() => {
          //   this.$message({
          //     type: 'success',
          //     message: '操作成功!'
          //   });
          // }).catch(() => {
          //   this.$message({
          //     type: 'info',
          //     message: '已取消'
          //   });          
          // });
          _this.$message({
            message:'此功能暂未开发，敬请期待',
            type:'warning'
          });
      },
      opsmg(row){               //所属运营格式化
        if(row.CustomedAttach==''||row.CustomedAttach==null){
          return '/';
        }else{
          return row.CustomedAttach;
        }
      },
      reset(type){                     //重置
        var _this=this;
        if(type=='name'){
          _this.index.searchname='';
          _this.GetAgentList(_this.index.currentPage,_this.index.pagesize,'');
        }else if(type=='time'){
          _this.index.time='';
          _this.GetCustomList(_this.index.pagesize,_this.index.currentPage,_this.detail.agentid,'','');
        }
      },
      reback(){              //返回按钮
        this.index.index=true;
        this.index.detail=false;
      },
      handleSizeChange(val) {    //分页功能
        this.index.pagesize=val;
        var _this=this;
        _this.index.currentPage=1;
        if(_this.index.index==true){
          _this.GetAgentList(_this.index.currentPage,_this.index.pagesize,'');
        }else if(_this.index.detail==true){
          _this.GetCustomList(_this.index.pagesize,_this.index.currentPage,_this.detail.agentid,'','');
        }
      },
      handleCurrentChange(val) {    //当前页改变
        var _this=this;
        _this.index.pagesize=val;
        if(_this.index.index==true){
          _this.GetAgentList(_this.index.currentPage,_this.index.pagesize,'');
        }else if(_this.index.detail==true){
          _this.GetCustomList(_this.index.pagesize,_this.index.currentPage,_this.detail.agentid,'','');
        }
      },
    },
    created(){
      var _this=this;
      var currentpage=_this.index.currentPage;
      var pagesize=_this.index.pagesize;
      _this.GetAgentList(currentpage,pagesize,'');
    },
  }
</script>

<style scoped>

</style>

<template>
  <div class="opsmg">
    <!--无权访问页面-->
    <div v-if="index.nopower" class="nopower"><h2>您无权访问此页面</h2></div>
    <!--主页-->
    <div v-else>
      <div v-show="index.main">
        <div class="opsbtn">
          <el-row :gutter="0" v-if="index.allbtn">
            <el-col :span="2" class="elcol">
              <el-button type="primary" :class="[this.index.onclick1?'onclick':'point']" @click="allcompy()">所有公司</el-button>
            </el-col>
            <el-col :span="2" class="elcol">
              <el-button type="primary" v-if="index.notallot" :class="[this.index.onclick2?'onclick':'point']" @click="assign()">未分配</el-button>
            </el-col>
            <el-col :span="2" class="elcol">
              <el-button type="primary" :class="[this.index.onclick3?'onclick':'point']" @click="oper()">运营管理</el-button>
            </el-col>
          </el-row>
        </div>

        <div class="opsearch" v-show="index.compny">
          <el-row>
            <el-col :span="2" class="eltag">
              <el-tag type="info" class="tag">公司名称：</el-tag>
            </el-col>
            <el-col :span="6" class="elinput" :offset="1">
              <el-input v-model="index.searchname" maxlength="50" ref="sername" class="search"
                placeholder="公司名称"  @keyup.enter.native="sername()" clearable>
              </el-input> 
            </el-col>
            <el-col :span="2" :offset="1">
              <el-button type="primary" @click="sername()">查询</el-button>
            </el-col>
            <el-col :span="2" :offset="1">
              <el-button type="primary" @click="reset('all')">重置</el-button>
            </el-col>
          </el-row>
        </div>

        <div class="opstable" v-show="index.compny">       <!--表格部分-->
          <el-table stripe border 
            :data="index.tabledata"
            :default-sort = "{prop: 'AccountBalance', order: 'descending'}">
            <el-table-column
              header-align="center"
              align="center"
              prop="CompanyName"
              label="公司名称">
            </el-table-column>
            <el-table-column
              header-align="center"
              align="center"
              prop="OpsDept"
              label="所属运营"
              :formatter="formatDept">
            </el-table-column>
            <el-table-column
              header-align="center"
              align="center"
              prop="AccountBalance"
              sortable
              label="账户余额">
            </el-table-column>
            <el-table-column
              header-align="center"
              align="center"
              prop="TotalConsumed"
              label="总消耗">
            </el-table-column>
            <el-table-column
              header-align="center"
              align="center"
              prop="MasterBuget"
              label="总预算">
            </el-table-column>
            <el-table-column
              header-align="center"
              align="center"
              prop="DayConsumed"
              label="日消耗">
            </el-table-column>
            <el-table-column
              header-align="center"
              align="center"
              label="操作"
              width="300px">
              <template slot-scope="scope">
                <el-button type="primary" @click="distribute(scope.row)" size="small">分配</el-button>
                <el-button type="primary" @click="details(scope.row)" size="small">详情</el-button>
                <el-button type="primary" @click="history(scope.row)" size="small">历史数据</el-button>
              </template>
            </el-table-column>
          </el-table>
        </div>
      </div>

      <!--运营管理界面-->
      <div v-show="opers.operation">
        <div class="opsearch">
          <el-row>
            <el-col :span="2" class="eltag">
              <el-tag type="info" class="tag">运营主管</el-tag>
            </el-col>

            <el-col :span='4' class="searsel">
              <el-select v-model="opers.select" :placeholder="opers.plholder" @change="change()">
                <el-option
                  v-for="item in opers.manager"
                  :key="item.OpsManagerName"
                  :label="item.OpsManagerName"
                  :value="item.OpsManagerName">
                </el-option>
              </el-select>
            </el-col>

            <el-col :span="2" class="eltag">
              <el-tag type="info" class="tag">运营名称</el-tag>
            </el-col>
            <el-col :span="6" class="elinput" :offset="1">
              <el-input v-model="opers.opsearchname" maxlength="50" ref="opsername" class="search" 
                placeholder="运营名称" @keyup.enter.native="opsername()"
                clearable>
              </el-input> 
            </el-col>
            <el-col :span="2" :offset="1">
              <el-button type="primary" @click="opsername()">查询</el-button>
            </el-col>
            <el-col :span="2" :offset="1">
              <el-button type="primary" @click="reset('oper')">重置</el-button>
            </el-col>
          </el-row>
        </div>

        <div class="opstable">       <!--表格部分-->
        <el-row>
          <el-table border
            :data="opers.optable" v-if="opers.seartabel">
            <el-table-column
              header-align="center"
              align="center"
              prop="OpsName"
              label="运营名称">
            </el-table-column>
            <el-table-column
              header-align="center"
              align="center"
              prop="OpsManagerName"
              label="运营主管">
            </el-table-column>
            <el-table-column
              header-align="center"
              align="center"
              prop="ToatlConsumed"
              label="总消耗">
            </el-table-column>
          </el-table>

          <el-table border
            :data="opers.sertable" v-else>
            <el-table-column
              header-align="center"
              align="center"
              prop="OpsName"
              label="运营名称">
            </el-table-column>
            <el-table-column
              header-align="center"
              align="center"
              prop="OpsManagerName"
              label="运营主管">
            </el-table-column>
            <el-table-column
              header-align="center"
              align="center"
              prop="ToatlConsumed"
              label="总消耗">
            </el-table-column>
          </el-table>

          <el-pagination class="pagin" v-if="opers.pagin"
            @size-change="handleSizeChange"
            @current-change="handleCurrentChange"
            :current-page.sync="opers.currentPage"
            :page-sizes="[2, 4, 6, 8]"
            :page-size="opers.pagesize"
            :pager-count="5"
            layout="total, sizes, prev, pager, next, jumper"
            :total="opers.total">
          </el-pagination>
        </el-row>
        </div>
      </div>

      <!--运营人员界面-->
      <div v-show="operman.index" class="operman">
        <el-row>
          <div class="maninfo">
            <el-col :span="2" class="eltag">
              <el-tag type="info" class="tag">运营名称</el-tag>
            </el-col>

            <el-col :span="3" class="elinput" :offset="1">
              <el-input v-model="operman.opname" class="search"
                readonly="readonly" clearable maxlength="50">
              </el-input> 
            </el-col>

            <el-col :span="2" class="eltag" :offset="2">
              <el-tag type="info" class="tag">运营主管</el-tag>
            </el-col>

            <el-col :span="3" class="elinput" :offset="1">
              <el-input v-model="operman.manager" class="search"
                readonly="readonly" clearable>
              </el-input> 
            </el-col>

            <el-col :span="2" class="eltag" :offset="2">
              <el-tag type="info" class="tag">总消耗</el-tag>
            </el-col>

            <el-col :span="3" class="elinput" :offset="1">
              <el-input v-model="operman.consume" class="search"
                readonly="readonly" clearable>
              </el-input> 
            </el-col>
          </div>
        </el-row>

          <el-row class="serach">
            <el-col :span="2" class="eltag">
              <el-tag type="info" class="tag">公司名称：</el-tag>
            </el-col>
            <el-col :span="6" class="elinput" :offset="1">
              <el-input v-model="operman.manane" ref="maname" maxlength="50" class="search"
                placeholder="公司名称"  @keyup.enter.native="mansearch()" clearable>
              </el-input> 
            </el-col>
            <el-col :span="2" :offset="1">
              <el-button type="primary" @click="mansearch()">查询</el-button>
            </el-col>
            <el-col :span="2" :offset="1">
              <el-button type="primary" @click="reset('operman')">重置</el-button>
            </el-col>
          </el-row>

        <el-row>
          <div class="table">
              <el-table stripe border 
                :data="operman.tabledata"
                :default-sort = "{prop: 'AccountBalance', order: 'descending'}">
                <el-table-column
                  header-align="center"
                  align="center"
                  prop="CompanyName"
                  label="公司名称">
                </el-table-column>
                <el-table-column
                  header-align="center"
                  align="center"
                  prop="OpsDept"
                  label="所属运营"
                  :formatter="formatDept">
                </el-table-column>
                <el-table-column
                  header-align="center"
                  align="center"
                  prop="AccountBalance"
                  sortable
                  label="账户余额">
                </el-table-column>
                <el-table-column
                  header-align="center"
                  align="center"
                  prop="TotalConsumed"
                  label="总消耗">
                </el-table-column>
                <el-table-column
                  header-align="center"
                  align="center"
                  prop="MasterBuget"
                  label="总预算">
                </el-table-column>
                <el-table-column
                  header-align="center"
                  align="center"
                  prop="DayConsumed"
                  label="日消耗">
                </el-table-column>
                <el-table-column
                  header-align="center"
                  align="center"
                  label="操作"
                  width="200px">
                  <template slot-scope="scope">
                    <el-button type="primary" @click="details(scope.row)" size="small">详情</el-button>
                    <el-button type="primary" @click="history(scope.row)" size="small">历史数据</el-button>
                  </template>
                </el-table-column>
              </el-table>

              <el-pagination class="pagin" v-if="operman.pagin"
                @size-change="handleSizeChanges"
                @current-change="handleCurrentChanges"
                :current-page.sync="operman.currentPage"
                :page-sizes="[2, 4, 6, 8]"
                :page-size="operman.pagesize"
                :pager-count="5"
                layout="total, sizes, prev, pager, next, jumper"
                :total="operman.total">
              </el-pagination>
          </div>
        </el-row>
      </div>

      <!--分配所属部门-->
      <div v-show="index.department" class="department">
        <el-row>
          <el-col :span="2" :offset="7" class="eltag">
            <el-tag type="info" class="tag">所属部门</el-tag>
          </el-col>

          <el-col :span='7' :offset="1" class="searsel">
              <el-select v-model="index.select" :placeholder="index.plholder">
                <el-option
                  v-for="item in index.section"
                  :key="item.account_id"
                  :label="item.staff_name"
                  :value="item.account_id">
                </el-option>
              </el-select>
            </el-col>
        </el-row>

        <el-row class="rowfooterbtn">
          <el-col :span="2" :offset="8" class="footerbtn">
            <el-button type="info" plain @click="reback()">返回</el-button>
          </el-col>
          <el-col :span="2" :offset="3" class="footerbtn">
            <el-button type="primary" @click="btnsure()">确认</el-button>
          </el-col>
        </el-row>
      </div>

      <!--历史数据页-->
      <div v-show="historydata.index" class="historydata">
        <el-row class="rowsearch">
          <el-col :span="2" class="eltag">
            <el-tag type="info" class="tag">时间段</el-tag>
          </el-col>
          
          <el-col :span="8" :offset="1" class="elinput">
            <el-date-picker
              v-model="historydata.times"
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
          <el-col :span="2" :offset="1" class="elserbtn">
            <el-button type="primary" @click="seartime()">查询</el-button>
          </el-col>
          <el-col :span="2" :offset="1" class="elserbtn">
            <el-button type="primary" @click="reset('time')">重置</el-button>
          </el-col>
          <el-col :span="2" :offset="6" class="elreback">
            <el-button type="primary" @click="hisreback()">返回</el-button>
          </el-col>
        </el-row>

        <el-row class="rowtable">
          <el-table stripe border 
                :data="historydata.tabledata"
                :default-sort = "{prop: 'RecordTime', order: 'descending'}">
                <el-table-column
                  header-align="center"
                  align="center"
                  prop="RecordTime"
                  label="时间"
                  sortable>
                </el-table-column>
              <el-table-column
                header-align="center"
                align="center"
                prop="DayConsumed"
                label="日消耗"
                sortable>
              </el-table-column>
            </el-table>
        </el-row>

        <el-row>
          <el-pagination class="pagin"  v-show="historydata.pagin"
            @size-change="hisSizeChange"
            @current-change="hisCurrentChange"
            :current-page.sync="historydata.currentPage"
            :page-sizes="[2, 4, 6, 8]"
            :page-size="historydata.pagesize"
            :pager-count="5"
            layout="total, sizes, prev, pager, next, jumper"
            :total="historydata.total">
          </el-pagination>
        </el-row>
      </div>

      <div v-show="index.pagin">
        <el-row>
          <el-pagination class="pagin"  v-show="index.pagin"
            @size-change="indexSizeChange"
            @current-change="indexCurrentChange"
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
  </div>
</template>

<script>
  import '../css/opsmg.less'
  import '../js/axios.js'
  export default {
    name: "OpsMg",
    data () {
      return {
        index:{    //主页
          compny:true,        //所有公司按钮v-show
          main:true,            //用于分配和主页v-show
          nopower:false,        //无权访问v-if
          allbtn:true,            //用于代理商的v-if
          notallot:true,           //未分配按钮
          onclick1:true,      //通过点击改变按钮样式
          onclick2:false,
          onclick3:false,
          department:false,
          select:'',             //分配部门下拉列表
          section:[],            
          tabledata:[],         //当前显示表格数据
          tableall:[],             //所有数据
          powerinfo:'',        //登录用户信息
          searchname:'',         //搜索框
          plholder:'',           //下拉框提示信息
          currentPage:1,         //分页
          pagesize:8,            //每页数量
          total:0,              //数据总数
          pagin:true,            //分页
          id:'',                  //分配参数
          customid:'',
        },
        opers:{      //运营管理
          operation:false,      // 运营管理按钮v-show 
          seartabel:true,        //table v-if
          opsearchname:'',          //运营管理搜索
          select:"",           //下拉选择
          plholder:'',             //下拉提示
          manager:[],           //运营主管列表
          optable:[],           //运营表格
          tableall:[],            //表格总数据
          sertable:[],               //搜索数组
          sertables:[],
          selectable:[],              //下拉数组
          currentPage:1,         //分页
          pagesize:8,            //每页数量
          total:0,              //数据总数
          pagin:false,            //分页
        },
        operman:{         //运营人员
          index:false,        //运营人员界面
          opname:'',          //运营名称
          manager:'',          //运营主管
          consume:'',          //总消耗
          manane:'',            //公司名称
          tabledata:[],           //表格数据
          pagesize:8,            //每页数量
          total:0,              //数据总数
          currentPage:1,         //默认页
          pagin:false,            //分页
        },
        historydata:{              //历史数据
          index:false,
          times:'',
          search:'',
          tabledata:[],
          customid:'',
          pagesize:8,            //每页数量
          total:0,              //数据总数
          currentPage:1,         //默认页
          pagin:true,            //分页
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
      seardAll(status,companyname,currentpage,pageSize){            //通过状态来查询
        var _this=this;
        var role=_this.index.powerinfo;
        _this.index.tabledata=[];
        _this.operman.tabledata=[];
        var params={};
        
        if(role=='Manager'){    //通过当前人员权限来传递不同参数
          if(companyname!=''||companyname!=null){    //用于搜索
            params={
              pageSize:pageSize,
              pageNo:(currentpage-1)*pageSize,
              companyName:companyname,
              opsStatus:status
            }
            
          }else{
            params={
              pageSize:pageSize,
              pageNo:(currentpage-1)*pageSize,
              opsStatus:status
            }
          }
        }else{
          if(companyname!=''||companyname!=null){
            params={
              pageSize:pageSize,
              pageNo:(currentpage-1)*pageSize,
              companyName:companyname,
            }
          }else{
            params={
              pageSize:pageSize,
              pageNo:(currentpage-1)*pageSize,
            }
          }
        }
        
        if(role=='Operation'){     //运营人员
          this.$http.get('/Admin/OpsMg/OpsDetail_vue',{   //查询表格
            params:params
          }).then(function(response){
            var info=response.data;
            if(info!=null||info!=''){
              _this.operman.tabledata=info.OpsMgModelListInfo;
              _this.operman.total=info.TotalCount;
              if(_this.index.total<=0){
                _this.index.pagin=false;
              }else{
                _this.index.pagin=true;
              }
            }else{
              _this.operman.tabledata=[];
              _this.operman.total=0;
            }
                
            if(_this.operman.pagin<=0){
              _this.operman.pagin=false;
            }else{
              _this.operman.pagin=true;
            }
          }).catch(function(error){
            _this.$message({
              message:'服务器错误',
              type:'error'
            });
          });
        }else if(role=='Manager'){           //超级管理员
          _this.$http.get('/Admin/OpsMg/ManagerDetail_vue',{   //查询表格
            params:params
          }).then(function(response){
            var info=response.data;
            if(info!=null||info!=''){
              _this.index.tabledata=info.OpsMgModelListInfo;
              _this.index.total=info.TotalCount;
            }else{
              _this.index.tabledata=[];
              _this.index.total=0;
            }
            if(_this.index.pagin<=0){
              _this.index.pagin=false;
            }else{
              _this.index.pagin=true;
            }   
          }).catch(function(error){
            _this.$message({
              message:'服务器错误',
              type:'error'
            });
          });
        }else if(role=='Agent'){     //代理商
          _this.$http.get('/Admin/OpsMg/AgentOpsDetail_vue',{   //查询表格
            params:params
          }).then(function(response){
            var info=response.data;
            if(info!=null||info!=''){
              _this.index.tabledata=info.OpsMgModelListInfo;
              _this.index.total=info.TotalCount;
            }else{
              _this.index.tabledata=[];
              _this.index.total=0;
            }
                
            if(_this.index.pagin<=0){
              _this.index.pagin=false;
            }else{
              _this.index.pagin=true;
            }   
          }).catch(function(error){
            _this.$message({
              message:'服务器错误',
              type:'error'
            });
          });  
        }else if(role=='OperationManager'){         //运营管理员
          _this.$http.get('/Admin/OpsMg/ManagerOpsDetail_vue',{   //查询表格
            params:params
          }).then(function(response){
            var info=response.data;
            if(info!=null||info!=''){
              _this.index.tabledata=info.OpsMgModelListInfo;
              _this.index.total=info.TotalCount;
            }else{
              _this.index.tabledata=[];
              _this.index.total=0;
            }
                
            if(_this.index.pagin<=0){
              _this.index.pagin=false;
            }else{
              _this.index.pagin=true;
            }   
          }).catch(function(error){
            _this.$message({
              message:'服务器错误',
              type:'error'
            });
          });
        }
      },
      historydatas(currentpage,pagesize,customid,startime,endtime){    //历史数据查询表格
        var params={};
        var _this=this;
        _this.historydata.tabledata=[];
        _this.historydata.currentPage=1;
        if(startime==''||endtime==''){    //通过参数来判断进行请求参数传递
          params={
            pageNo:currentpage,
            pageSize:pagesize,
            customId:customid,
          };
        }else{
          params={
            pageNo:currentpage,
            pageSize:pagesize,
            customId:customid,
            startTime:startime,
            endTime:endtime
          };
        }

        _this.$http.get('/Admin/OpsMg/GetCustonDayInfo_vue',{
          params:params
        }).then(function(response){
          var info=response.data;
          if(info!=''||info!=null){
            _this.historydata.total=info.TotalCount;
            _this.historydata.tabledata=info.OpsMgModelListInfo;
          }else{
            _this.historydata.total=0;
            _this.historydata.tabledata=[];
          }
          if(_this.historydata.total<=0){
            _this.historydata.pagin=false;
          }else{
            _this.historydata.pagin=true;
          }
        }).catch(function(error){
          console.log(error);
          _this.$message({
            message:'服务器错误',
            type:'error'
          });
        });
      },
      Getoperlist(){           //获取运营管理列表
        this.opers.optable=[];
        this.opers.selectable=[];
        this.opers.tableall=[];
        this.opers.seartabel=[];
        this.opers.total=0;

        //运营主管列表
        var _this=this;
        this.$http.post('/Admin/OpsMg/GetOpsManagerList').then(function(response){
            var info=JSON.parse(response.data.info);
            
            var opsinfolist=[];
            for (let index = 0; index < info.length; index++) {
              opsinfolist.push(info[index].OpsInfoList);
            }
            // console.log(opsinfolist);
          if(info.length>0){
            _this.opers.manager=info;
            _this.opers.select=_this.opers.manager[0].OpsManagerName;
            if(info.OpsInfoList===null){
              console.log(opsinfolist);
            }else{
              for (let index = 0; index < opsinfolist.length; index++) {
                for (let indexs = 0; indexs < opsinfolist[index].length; indexs++) {
                  _this.opers.tableall.push(opsinfolist[index][indexs]);
                  if(opsinfolist[index][indexs]==null){

                  }else{
                    if(opsinfolist[index][indexs].OpsManagerName==_this.opers.select){
                      _this.opers.selectable.push(opsinfolist[index][indexs]);
                    }
                  }
                }
                      
              }
            }

            if(_this.opers.selectable.length<_this.opers.pagesize){
              for (let index = 0; index < _this.opers.selectable.length; index++) {
                _this.opers.optable.push(_this.opers.selectable[index]);
              }
            }else{
              for (let index = 0; index < _this.opers.pagesize; index++) {
                _this.opers.optable.push(_this.opers.selectable[index]);
              }
            }

            _this.opers.total=_this.opers.selectable.length;
            if(_this.opers.total>_this.opers.pagesize){
              _this.opers.pagin=true;
              _this.index.pagin=false;
            }else{
              _this.opers.pagin=false;
            }
          }else{
            _this.opers.plholder='暂无运营主管';
            _this.opers.select='';
            _this.opers.optable=[];
          }
        }).catch(function(error){
          console.log(error);
          _this.$message({
            message:'服务器错误',
            type:'error'
          });
        });
      },
      allcompy(){        //所有公司
        this.index.onclick1=true;
        this.index.onclick2=false;
        this.index.onclick3=false;
        this.index.compny=true;
        this.opers.operation=false;
        this.index.pagin=true;
        this.opers.pagin=false;
        this.operman.pagin=false;
        this.index.pagesize=8;
        this.index.currentPage=1;
        this.seardAll('false','',this.index.currentPage,this.index.pagesize);
      },
      assign(){          //未分分配
        this.index.onclick1=false;
        this.index.onclick2=true;
        this.index.onclick3=false;
        this.index.compny=true;
        this.opers.operation=false;
        this.index.pagin=true;
        this.opers.pagin=false;
        this.operman.pagin=false;
        this.index.pagesize=8;
        this.seardAll('true','',this.index.currentPage,this.index.pagesize)
      },
      oper(){              //运营管理
        this.index.onclick1=false;
        this.index.onclick2=false;
        this.index.onclick3=true;
        this.index.compny=false;
        this.opers.operation=true;
        this.opers.seartabel=true;
        this.index.pagin=false;
        this.opers.pagin=true;
        this.operman.pagin=false;
        this.index.pagesize=8;
        this.Getoperlist();
      },
      sername(){           //搜索方法
        if(this.index.searchname==''){
          this.$message({
            message:'请输入要查询的公司名称',
            type:'warning'
          });
          this.$refs.sername.focus();
          return;
        }else{
          var _this=this;
          if(_this.index.onclick1==true){
            _this.seardAll('false',_this.index.searchname,_this.index.currentPage,_this.index.pagesize);
          }else{
            _this.seardAll('true',_this.index.searchname,_this.index.currentPage,_this.index.pagesize);
          }
          _this.index.searchname=='';
        }
      },
      reset(type){               //重置
        var _this=this;
        var state='false';
        if(type=='all'){            //所有公司和未分配
          _this.index.searchname='';
          if(_this.index.onclick1==true){
            state='false';
            _this.seardAll(state,'',_this.index.currentPage,_this.index.pagesize);
          }else if(_this.index.onclick2==true){
            state='true';
            _this.seardAll(state,'',_this.index.currentPage,_this.index.pagesize);
          }
        }else if(type=='oper'){                 //运营管理
          _this.opers.opsearchname='';
          _this.Getoperlist();
        }else if(type=='operman'){
          _this.operman.manane='';
          if(_this.index.onclick1==true){
            state='false';
            _this.seardAll(state,'',_this.index.currentPage,_this.index.pagesize);
          }else if(_this.index.onclick2==true){
            state='true';
            _this.seardAll(state,'',_this.index.currentPage,_this.index.pagesize);
          }
        }else if(type=='time'){
          _this.historydata.times='';
          _this.historydatas(_this.historydata.currentPage,_this.historydata.pagesize,_this.historydata.customid,'','');
        }
      },
      opsername(){            //运营管理搜索
        this.opers.seartabel=false;
        this.opers.sertable=[];
        this.opers.sertables=[];
        this.opers.total=0;
        var _this=this;
        if(this.opers.opsearchname==''){
          this.$message({
            message:'请输入要查询的运营名称',
            type:'warning'
          });
          this.$refs.opsername.focus();
          return;
        }
        for (let index = 0; index < this.opers.selectable.length; index++) {
          if(this.opers.selectable[index]==null){
            
          }else{
            if(this.opers.selectable[index].OpsName==this.opers.opsearchname){
              this.opers.sertables.push(this.opers.selectable[index]);
            }
          }
         
        }

        if(_this.opers.sertables.length<_this.opers.pagesize){
          for (let index = 0; index < _this.opers.sertables.length; index++) {
            _this.opers.sertable.push(_this.opers.sertables[index]);
          }
        }else{
          for (let index = 0; index < _this.opers.pagesize; index++) {
            _this.opers.sertable.push(_this.opers.sertables[index]);
          }
        }
        
        this.opers.total=this.opers.sertables.length;
        if(this.opers.total>this.opers.pagesize){
          this.opers.pagin=true;
          _this.index.pagin=false;
        }else{
            _this.opers.pagin=false;
          }
        this.opers.opsearchname='';
      },
      distribute(row){          //所属部门分配
        var _this=this;
        _this.index.main=false;
        _this.index.department=true;
        _this.index.pagin=false;
        _this.index.id=row.Id;
        _this.index.customid=row.CustomId;
      },
      details(row){          //详情
        var _this=this;
        _this.$http.get('/Admin/OpsMg/RedirectCustomDetail',{
          params:{
            customId:row.CustomId
          }
        }).then(function(response){
          var url=response.data.urlInfo;
          var info=JSON.parse(response.data.custom);
          if(response.data.statusCode==1001){
            _this.$http.post(url,info).then(function(response){
              if(response.data.status=='ok'){
                window.open('/index.html#/CustomHome');
              }else{
                _this.$message({
                  message:'服务器错误',
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
          }else if(response.data.statusCode==1103){
            _this.$message({
              message:response.data.info,
              type:'error'
            });
          }else if(response.data.statusCode==1105){
            _this.$message({
              message:'服务器繁忙',
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
      },
      history(row){          //历史数据
        var _this=this;
        _this.historydata.customid=row.CustomId;
        _this.index.main=false;
        _this.historydata.index=true;
        _this.index.pagin=false;
        _this.opers.pagin=false;
        var currentPage=_this.historydata.currentPage;
        var pagesize=_this.historydata.pagesize;
        var customid=_this.historydata.customid;
        _this.historydatas(currentPage,pagesize,customid,'','');
      },
      seartime(){               //历史数据查询
        var _this=this;
        var currentPage=_this.historydata.currentPage;
        var pagesize=_this.historydata.pagesize;
        var customid=_this.historydata.customid;
        if(this.historydata.times==''){
          _this.$message({
            message:'请选择要查询的时间段',
            type:'warning'
          });
          return;
        }else{
          console.log(_this.historydata.times);
          var startime=_this.historydata.times[0];
          var endtime=_this.historydata.times[1];
          _this.historydatas(currentPage,pagesize,customid,startime,endtime);
        }
      },
      hisreback(){
        this.historydata.index=false;
        this.index.main=true;
      },
      formatDept(row,column,cellValue){    //所属运营的判断
        if(cellValue===''||cellValue===null){
          return '/';
        }else{
          return cellValue;
        }
      },
      mansearch(){        //运营人员搜索
        if(this.operman.manane==''){
          this.$message({
            message:'请输入要查询的公司名称',
            type:'warning'
          });
          this.$refs.maname.focus();
          return;
        }else{
          var _this=this;
          _this.$http.get('/Admin/OpsMg/OpsDetail_vue',{   //查询表格
            params:{
              pageSize:_this.operman.pagesize,
              pageNo:_this.operman.currentPage,
              companyName:_this.operman.manane
            }
          }).then(function(response){
            var infos=response.data;
            if(infos!=null||infos!=''){
              _this.operman.tabledata=infos.OpsMgModelListInfo;
              _this.operman.total=infos.TotalCount;   
              if(_this.operman.total<=0){
                _this.operman.pagin=false;
              }else{
                _this.operman.pagin=true;
              }
            }else{
              _this.operman.tabledata=[];
              _this.operman.total=0;   
            }
          }).catch(function(error){
            _this.$message({
              message:'服务器错误',
              type:'error'
            });
          });
          _this.operman.manane='';
        }
      },
      change(){       //下拉选择
        var _this=this;
        _this.opers.seartabel=true;
        _this.opers.optable=[];
        _this.opers.selectable=[];
        _this.opers.currentPage=1;
        _this.opers.total=0;
        
        for (let index = 0; index < _this.opers.tableall.length; index++) {
          if(_this.opers.tableall[index]==null){
            
          }else{
            if(_this.opers.tableall[index].OpsManagerName==_this.opers.select){
              _this.opers.selectable.push(_this.opers.tableall[index]);
            }
          }
        }

        if(_this.opers.selectable.length<_this.opers.pagesize){
          for (let index = 0; index < _this.opers.selectable.length; index++) {
            _this.opers.optable.push(_this.opers.selectable[index]);
          }
        }else{
          for (let index = 0; index < _this.opers.pagesize; index++) {
            _this.opers.optable.push(_this.opers.selectable[index]);
          }
        }
        
        _this.opers.total=_this.opers.selectable.length;
        
          if(_this.opers.total>_this.opers.pagesize){
            _this.opers.pagin=true;
            _this.index.pagin=false;
          }else{
            _this.opers.pagin=false;
          }
      },
      reback(){      //分配取消
        this.index.department=false;
        this.operman.index=false;
        this.index.main=true;
        this.index.pagin=true;
        this.oper.pagin=false;
        this.operman.pagin=false;
      },
      btnsure(){        //确认分配
        if(this.index.select==''){
          this.$message({
            message:'请选择所属部门',
            type:'warning'
          });
          return;
        }else{
          var personid='';
          var _this=this;
          for (let index = 0; index < _this.index.section.length; index++) {
            if(_this.index.section[index].staff_name==_this.index.select){
              _this.index.select=_this.index.section[index].account_id;
            }
          }
          _this.$http.post('/Admin/OpsMg/ModifyOpsPerson',{
            id:_this.index.id,
            customId:_this.index.customid,
            opsPersonId:_this.index.select
          }).then(function(response){
            if(response){
              _this.$message({
                message:'更新成功',
                type:'success'
              });
              setTimeout(function(){
                _this.index.department=false;
                _this.index.main=true;
                _this.index.pagin=true;
                _this.operman.pagin=false;
                _this.oper.pagin=false;
                if(_this.index.onclick1==true){
                  _this.seardAll('false','',_this.index.currentPage,_this.index.pagesize);
                }else if(_this.index.onclick2==true){
                  _this.seardAll('true','',_this.index.currentPage,_this.index.pagesize);
                }
              },1300);
            }else{
              _this.$message({
                message:'更新失败',
                type:'error'
              });
            }
          }).catch(function(error){
            _this.$message({
              message:'服务器错误',
              type:'error'
            });
          });
        }
      },
      handleSizeChange(val) {    //运营管理每页个数
        this.opers.pagesize=val;
          this.opers.optable=[];
            this.opers.currentPage=1;
          if(this.opers.total<val){
            for (let index = 0; index < this.opers.selectable.length; index++) {
              this.opers.optable.push(this.opers.selectable[index]);
            }
          }else{
            for (let index = 0; index < val; index++) {
              this.opers.optable.push(this.opers.selectable[index]);
            }
          }
      },
      handleCurrentChange(val) {    //运营管理当前页改变
        var _this=this;
          this.opers.optable=[];
          var startPage=this.opers.pagesize*(val-1)+1;
          var endPage=this.opers.pagesize*val;
          var result=0;
          if(this.opers.total/this.opers.pagesize>parseInt(this.opers.total/this.opers.pagesize)){
            result=parseInt(_this.opers.total/_this.opers.pagesize)+1;
          }else{
            result=parseInt(this.opers.total/this.opers.pagesize);
          }

          //判断最后页的数据够不够整页渲染
          if(val==result){
            for (let index = startPage-1; index < _this.opers.selectable.length; index++) {
              _this.opers.optable.push(_this.opers.selectable[index]);
            }
          }else{
            for (let index = startPage-1; index <= endPage-1; index++) {
              _this.opers.optable.push(_this.opers.selectable[index]);
            }
          }
      },
      handleSizeChanges(val) {    //运营人员每页个数
        var _this=this;
        _this.operman.pagesize=val;
        _this.seardAll('','','',_this.operman.currentPage,_this.operman.pagesize);
      },
      handleCurrentChanges(val) {    //运营人员当前页改变
        var _this=this;
        _this.operman.currentPage=val;
        _this.seardAll('','','',_this.operman.currentPage,_this.operman.pagesize);
      },
      indexSizeChange(val){           //首页每页个数
        var _this=this;
        _this.index.pagesize=val;
        if(_this.index.onclick1==true){    //通过判断按钮传递参数
          _this.seardAll('false','',_this.index.currentPage,_this.index.pagesize);
        }else if(_this.index.onclick2==true){
          _this.seardAll('true','',_this.index.currentPage,_this.index.pagesize);
        }
      },
      indexCurrentChange(val){          //首页当前页改变
        var _this=this;
        _this.index.currentPage=val;
        if(_this.index.onclick1==true){    //通过判断按钮传递参数
          _this.seardAll('false','',_this.index.currentPage,_this.index.pagesize);
        }else if(_this.index.onclick2==true){
          _this.seardAll('true','',_this.index.currentPage,_this.index.pagesize);
        }
      },
      hisCurrentChange(val){           //历史数据每页个数
        var _this=this;
        _this.historydata.currentPage=val;
        var currentPage=_this.historydata.currentPage;
        var pagesize=_this.historydata.pagesize;
        var customid=_this.historydata.customid;
        _this.historydatas(currentPage,pagesize,customid,'','');
      },
      hisSizeChange(val){              //历史数据当前页改变
        var _this=this;
        _this.historydata.pagesize=val;
        var currentPage=_this.historydata.currentPage;
        var pagesize=_this.historydata.pagesize;
        var customid=_this.historydata.customid;
        _this.historydatas(currentPage,pagesize,customid,'','');
      },
    },
    created(){
      var _this=this;

      //权限检查
      this.$http.post('/Admin/OpsMg/CheckRole').then(function(response){
        _this.index.powerinfo=response.data.info;
        if(response.data.statusCode=='1002'){
          _this.index.nopower=true;
          return;
        }else{
          _this.index.pagesize=8;
          _this.index.currentPage=1;
          _this.index.total=0;

          _this.$http.post('/Admin/OpsMg/GetOps').then(function(response){
                var info=response.data;
                var params={};
                if(info!=''||info!=null){
                  _this.operman.opname=info.OpsName;
                  _this.operman.manager=info.ManagerName;
                  if(info.IsOpsRole=='OpsDetail'){
                    params={
                      pageSize:_this.operman.pagesize,
                      pageNo:(_this.operman.currentPage-1)*_this.operman.pagesize
                    };
                  }else{
                    params={
                      pageSize:_this.index.pagesize,
                      pageNo:(_this.index.currentPage-1)*_this.index.pagesize
                    };
                  }
                  _this.$http.get('/Admin/OpsMg/'+info.OpsReq+'_vue',{   //根据不同的权限接口查询表格
                    params:params
                  }).then(function(response){
                    var infos=response.data;
                    var consume='';
                    var num=0;
                    if(infos!=null||infos!=''){
                      if(infos.OpsMgModelListInfo.length>0){
                        for (let index = 0; index < infos.OpsMgModelListInfo.length; index++) {
                          consume=infos.OpsMgModelListInfo[index].TotalConsumed.replace('￥','');
                          consume=parseInt(consume);
                          num=parseInt(num)+consume;
                        }
                      }else{
                        num=0;
                      }
                      _this.operman.consume=num+'元';
                      if(info.IsOpsRole=='OpsDetail'){     //不同权限渲染不同表格
                        _this.operman.tabledata=infos.OpsMgModelListInfo;
                        _this.operman.total=infos.TotalCount;
                      }else if(info.IsOpsRole==''||info.IsOpsRole==null){
                        _this.index.tabledata=[];
                        _this.index.total=0;
                        _this.operman.tabledata=[];
                        _this.operman.total='';
                      }else{
                        _this.index.tabledata=infos.OpsMgModelListInfo;
                        _this.index.total=infos.TotalCount;
                      }
                      
                      if(_this.index.total<=0){     //用于判断分页
                        _this.index.pagin=false;
                      }else if(_this.operman.total<=0){
                        _this.operman.pagin=false;
                      }else{
                        _this.index.pagin=true;
                        _this.operman.pagin=true;
                      }
                    }else{
                      _this.index.tabledata=[];
                      _this.index.total=0;
                      _this.operman.tabledata=[];
                      _this.operman.total='';
                    }
                    
                  }).catch(function(error){
                    _this.$message({
                      message:'服务器错误',
                      type:'error'
                    });
                  });
                }else{
                  _this.index.tabledata=[];
                  _this.index.total=0;
                  _this.operman.tabledata=[];
                  _this.operman.total='';
                  _this.operman.opname='';
                  _this.operman.manager='';
                }
                
                
          }).catch(function(error){
            _this.$message({
              message:'服务器错误',
              type:'error'
            });
          });
          if(response.data.info=='Operation'){    //运营人员
            _this.index.main=false;
            _this.opers.operation=false;
            _this.operman.index=true;
            _this.index.pagin=true;
            return;
          }else if(response.data.info=='Manager'||response.data.info=='Agent'||response.data.info=='OperationManager'){  //超级管理、代理商、运营管理员
            if(response.data.info=='Manager'){     //超级管理员
              _this.index.notallot=true;
              _this.index.allbtn=true;
            }else if(response.data.info=='Agent'){    //代理商
              _this.index.notallot=false; 
              _this.index.allbtn=false;
            }else if(response.data.info=='OperationManager'){     //运营管理员
              _this.index.notallot=false;
              _this.index.allbtn=true;
            }

            _this.index.main=true;
            _this.opers.operation=false;
            _this.operman.index=false;
            _this.index.pagin=true;
            _this.opers.seartabel=true;

            //查询所有部门用来分配所属
            _this.$http.post('/Admin/OpsMg/GetOpsList').then(function(response){
              if(response.data!=0){
                var info=JSON.parse(response.data);
                for (var i in info) {
                  _this.index.section.push(info[i]);
                }
                _this.index.plholder='请选择所属部门'
              }else{
                _this.index.plholder='暂无部门选择';
                _this.index.select='';
              }
            }).catch(function(error){
              console.log(error);
              _this.$message({
                message:'服务器错误',
                type:'error'
              });
            });
          }
        }
      }).catch(function(error){
        console.log(error);
        _this.$message({
          message:'服务器错误',
          type:'error'
        });
      });
      
    },
  }
</script>

<style scoped>

</style>
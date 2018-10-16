<template>
    <div class="agentfinance">
        <!--客户财务信息总计-->
        <el-row class="row_allmoney">
            <el-col :span="8" class="el_allinfo">
                <span>客户财务信息总计：</span>
            </el-col>
            <el-col :span="8" class="el_rechargecount">
                <span>现金余额：{{infos.rechargecount}}</span>
            </el-col>
            <el-col :span="8" class="el_consumed">
                <span>今日花费：{{infos.consumed}}</span>
            </el-col>
        </el-row>
        <el-row class="row_moneyinfo">
            <el-col :span="8" class="el_accountitle moneyspan">
                <span>现金账户</span>
            </el-col>
            <el-col :span="8" class="el_rebateaccountitle moneyspan">
                <span>返货账户</span>
            </el-col>
            <el-col :span="8" class="el_legalcustomtitle moneyspan">
                <span>有效客户</span>
            </el-col>
        </el-row>
        <el-row class="row_moneyinfos">
            <el-col :span="8" class="el_account moneyspan">
                <span>{{infos.account}}</span>
            </el-col>
            <el-col :span="8" class="el_rebateaccount moneyspan">
                <span>{{infos.rebateaccount}}</span>
            </el-col>
            <el-col :span="8" class="el_legalcustom moneyspan">
                <span>{{infos.legalcustom}}</span>
            </el-col>
        </el-row>

        <!--按钮部分-->
        <el-row class="row_listbtn">
            <el-col :span="3" class="elcol">
                <el-button type="primary" :class="[this.accountlist.index?'onclick':'point']" @click="customlists()">客户列表</el-button>
            </el-col>
            <el-col :span="3" class="elcol" :offset="1">
                <el-button type="primary" :class="[this.customwoter.btnclick?'onclick':'point']" @click="customwolists()">客户财务流水</el-button>
            </el-col>
            <el-col :span="3" class="elcol" :offset="1">
                <el-button type="primary" :class="[this.agentwoter.btnclick?'onclick':'point']" @click="agentwolists()">代理商财务流水</el-button>
            </el-col>
        </el-row>

        <!--客户列表-->
        <div v-show="accountlist.index">
            <el-row class="row_searchname">
                <el-col :span="2" class="el_select">
                    <el-select class="agentitem" v-model="accountlist.agentid" :placeholder="accountlist.placeholder" @change="agentitemchange()">
                        <el-option
                            v-for="item in accountlist.agentlist"
                            :key="item.AccountId"
                            :label="item.StaffName"
                            :value="item.AccountId">
                        </el-option>
                    </el-select>
                </el-col>
                <el-col :span="2" class="eltag" :offset="1">
                    <el-tag type="info" class="tag">公司名称</el-tag>
                </el-col>
                <el-col :span="5" class="elinput" :offset="1">
                    <el-input v-model="accountlist.name"
                        @keyup.enter.native="search('name')"
                        ref="sername" class="search" 
                        placeholder="输入名称进行搜索" clearable>
                    </el-input> 
                </el-col>
                <el-col :span="1" :offset="1" class="el_searchbtn">
                    <el-button type="primary" @click="search('name')">查询</el-button>
                </el-col>
                <el-col :span="1" :offset="1" class="el_searchbtn">
                    <el-button type="primary" @click="reset('name','cuslists')">重置</el-button>
                </el-col>
            </el-row>

            <el-row class="row_table">
                <el-table border 
                    :data="accountlist.tabledata"
                    :default-sort = "{prop: 'CustomName', order: 'descending'}">
                    <el-table-column
                        header-align="center"
                        align="center"
                        prop="CustomId"
                        label="ID">
                    </el-table-column>
                    <el-table-column 
                        header-align="center"
                        align="center"
                        prop="CustomName"
                        sortable
                        label="公司名称">
                    </el-table-column>
                    <el-table-column 
                        header-align="center"
                        align="center"
                        prop="CustomStatus"
                        label="状态">
                    </el-table-column>
                    <el-table-column 
                        header-align="center"
                        align="center"
                        prop="CashCount"
                        label="现金余额">
                    </el-table-column>
                    <el-table-column 
                        header-align="center"
                        align="center"
                        prop="CustomTodayTotalConsumed"
                        label="今日花费"
                        sortable>
                    </el-table-column>
                    <el-table-column
                        header-align="center"
                        align="center"
                        label="操作"
                        width="200px">
                    <template slot-scope="scope">
                        <el-button type="primary" @click="flowmoney(scope.row,'in')">转入资金</el-button>
                        <el-button type="primary" @click="flowmoney(scope.row,'to')">转出资金</el-button>
                    </template>
                    </el-table-column>
                </el-table>
            </el-row>

            <el-row class="row_pagin">
                <el-pagination class="pagin" v-show="accountlist.pagin"
                    @size-change="customSizeChange"
                    @current-change="customCurrentChange"
                    :current-page.sync="accountlist.currentPage"
                    :page-sizes="[2, 4, 6,8]"
                    :page-size="accountlist.pagesize"
                    :pager-count="5"
                    layout="total, sizes, prev, pager, next, jumper"
                    :total="accountlist.total">
                </el-pagination>
            </el-row>
        </div>

        <!--财务流水列表-->
        <div v-show="customwoter.index">
            <el-row class="row_searchtime">
                <el-col :span="2" class="el_select">
                    <el-select class="agentitem" v-model="accountlist.agentid" :placeholder="accountlist.placeholder" @change="agentitemchange()">
                        <el-option
                            v-for="item in accountlist.agentlist"
                            :key="item.AccountId"
                            :label="item.StaffName"
                            :value="item.AccountId">
                        </el-option>
                    </el-select>
                </el-col>
                <el-col :span="2" class="eltag" :offset="1">
                    <el-tag type="info" class="tag">时间段</el-tag>
                </el-col>
                <el-col :span="8" class="elinput" :offset="1">
                    <el-date-picker
                    v-model="customwoter.time"
                    type="daterange"
                    align="left"
                    unlink-panels
                    range-separator="至"
                    start-placeholder="开始日期"
                    end-placeholder="结束日期"
                    :picker-options="timers"
                    format="yyyy 年 MM 月 dd 日"
                    value-format="yyyy/MM/dd">
                    </el-date-picker>
                </el-col>
                 <el-col :span="1" :offset="1" class="el_searchbtn">
                    <el-button type="primary" @click="search('times')">查询</el-button>
                </el-col>
                <el-col :span="1" :offset="1" class="el_searchbtn">
                    <el-button type="primary" @click="reset('times','cuswoter')">重置</el-button>
                </el-col>
            </el-row>

            <el-row class="row_table">
                <el-table border 
                    :data="customwoter.tabledata"
                    :default-sort = "{prop: 'RecordTime', order: 'descending'}">
                    <el-table-column
                        header-align="center"
                        align="center"
                        :formatter="wotertime"
                        time-format="yyyy-MM-dd HH:mm:ss"
                        sortable
                        prop="RecordTime"
                        label="时间"
                        >
                    </el-table-column>
                    <el-table-column 
                        header-align="center"
                        align="center"
                        prop="CustomName"
                        label="公司名称">
                    </el-table-column>
                    <el-table-column 
                        header-align="center"
                        align="center"
                        prop="ApplyPerson"
                        label="操作人">
                    </el-table-column>
                    <el-table-column 
                        header-align="center"
                        align="center"
                        prop="TransicationKey"
                        label="流水编号">
                    </el-table-column>
                    <el-table-column 
                        header-align="center"
                        align="center"
                        prop="OperationFunType"
                        label="操作类型">
                    </el-table-column>
                    <el-table-column 
                        header-align="center"
                        align="center"
                        prop="MarkText"
                        label="备注"
                        :formatter="woterRemark">
                    </el-table-column>
                    <el-table-column 
                        header-align="center"
                        align="center"
                        prop="TotalAmount"
                        label="金额（元）">
                    </el-table-column>
                </el-table>
            </el-row>

            <el-row class="row_pagin">
                <el-pagination class="pagin" v-show="accountlist.pagin"
                    @size-change="woterSizeChange"
                    @current-change="woterCurrentChange"
                    :current-page.sync="customwoter.currentPage"
                    :page-sizes="[2, 4, 6,8]"
                    :page-size="customwoter.pagesize"
                    :pager-count="5"
                    layout="total, sizes, prev, pager, next, jumper"
                    :total="customwoter.total">
                </el-pagination>
            </el-row>
        </div>

        <!--转入资金模态框-->
        <el-dialog
            title="转入资金"
            :visible.sync="modelin.index"
            width="500px">
            <div class="dialog_money spans">
                <span>金额：</span>
                <el-input v-model="modelin.money" ref="inmoney" clearable placeholder="请输入金额"></el-input>
                <span class="span_can">可用金额：</span>
                <span class="span_can">{{modelin.accountmoney}}</span>
            </div>
            <div class="dialog_from spans">
                <span>来源：</span>
                <el-select v-model="modelin.selectfrom" @change="moneyfromitem()">
                    <el-option
                        v-for="item in modelin.selectfromlist"
                        :key="item.value"
                        :label="item.name"
                        :value="item.value">
                    </el-option>
                </el-select>
            </div>
            <div class="dialog_to spans">
                <span>目的：</span>
                <el-select v-model="modelin.selectto">
                    <el-option
                        v-for="item in modelin.selecttolist"
                        :key="item.value"
                        :label="item.name"
                        :value="item.value">
                    </el-option>
                </el-select>
            </div>
            <div class="dialog_remark spans">
                <span>备注：</span>
                <el-input
                    type="textarea"
                    :autosize="{ minRows: 5}"
                    placeholder="请输入备注"
                    v-model="modelin.remark">
                </el-input>
            </div>
            <span slot="footer" class="dialog-footer">
                <el-button @click="modelin.index = false">取 消</el-button>
                <el-button type="primary" @click="moneyupdate('in')">确 定</el-button>
            </span>
        </el-dialog>

        <!--转出资金模态框-->
        <el-dialog
            title="转出资金"
            :visible.sync="modelto.index"
            width="500px">
            <div class="dialog_money spans">
                <span>金额：</span>
                <el-input v-model="modelto.money" ref="outmoney" clearable placeholder="请输入金额"></el-input>
                <span class="span_can">可用金额：</span>
                <span class="span_can">{{modelto.accountmoney}}</span>
            </div>
            <div class="dialog_from spans">
                <span>来源：</span>
                <el-select v-model="modelto.selectfrom">
                    <el-option
                        v-for="item in modelto.selectfromlist"
                        :key="item.value"
                        :label="item.name"
                        :value="item.value">
                    </el-option>
                </el-select>
            </div>
            <div class="dialog_to spans">
                <span>目的：</span>
                <el-select v-model="modelto.selectto">
                    <el-option
                        v-for="item in modelto.selecttolist"
                        :key="item.value"
                        :label="item.name"
                        :value="item.value">
                    </el-option>
                </el-select>
            </div>
            <div class="dialog_remark spans">
                <span>备注：</span>
                <el-input
                    type="textarea"
                    :autosize="{ minRows: 5}"
                    placeholder="请输入备注"
                    v-model="modelto.remark">
                </el-input>
            </div>
            <span slot="footer" class="dialog-footer">
                <el-button @click="modelto.index = false">取 消</el-button>
                <el-button type="primary" @click="moneyupdate('to')">确 定</el-button>
            </span>
        </el-dialog>
    </div>
</template>

<script>
    import '../css/agentfinance.less'
    import '../js/axios.js'
  export default {
    name: 'AgentFinance',
    data() {
      return {
        infos:{           //客户财务信息总计
            rechargecount:'0元',          //现金余额
            consumed:'0元',               //今日花费
            account:'0元',                //现金账户
            rebateaccount:'0元',          //返货账户
            legalcustom:'0个',            //有效客户
        },
        accountlist:{          //账户列表
            index:true,
            agentid:'',            //代理商绑定值
            agentlist:[],           //代理商列表
            name:'',                //搜索名称
            tabledata:[],           //表格数据
            customid:'',              //客户ID
            placeholder:'暂无代理商',             //代理商下拉
            moneyinfos:'',                 //转出资金剩余
            currentPage:1,            //默认第几页
            pagesize:6,            //每页数量
            total:0,              //数据总数
            pagin:true,            //分页
        },
        customwoter:{
            index:false,
            btnclick:false,
            time:'',            //搜索时间
            tabledata:[],         //表格数据
            currentPage:1,            //默认第几页
            pagesize:6,            //每页数量
            total:0,              //数据总数
            pagin:true,            //分页
        },
        agentwoter:{
            index:false,
            btnclick:false,           //代理商财务按钮
            time:'',
            tabledata:[],
        },
        modelin:{             //转入资金模态框
            index:false,
            money:'',        //操作金额
            accountmoney:'',        //剩余金额
            selectfrom:'1',      //来源下拉
            selectfromlist:[
                {value:'1',name:'现金'},
                {value:'2',name:'返货'}
            ],
            selectto:'1',      //目地下拉
            selecttolist:[
                {value:'1',name:'现金'}
            ],
            remark:'',            //备注
        },
        modelto:{              //转出资金模态框
            index:false,
            money:'',        //操作金额
            accountmoney:'',        //剩余金额
            selectfrom:'1',      //来源下拉
            selectfromlist:[
                {value:'1',name:'现金'}
            ],
            selectto:'1',      //目地下拉
            selecttolist:[
                {value:'1',name:'现金'}
            ],
            remark:'',            //备注
        },
        timers: {       //日期快捷选择
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
        GetAgentLists(){           //获取代理商列表
            var _this=this;
            _this.$http.post('/Admin/AgentFinance/GetAgentList').then(function(response){
                var info=response.data;
                if(info!=''||info!=null){
                    if(info.length>0){
                        _this.accountlist.agentlist=info;
                        _this.accountlist.agentid=_this.accountlist.agentlist[0].AccountId;
                        _this.GetcustomInfo(_this.customwoter.pagesize,_this.customwoter.currentPage,'');
                    }else{
                        _this.accountlist.agentlist=[];
                        _this.accountlist.placeholder='暂无代理商';
                    }
                }else{
                    _this.accountlist.agentlist=[];
                    _this.accountlist.placeholder='暂无代理商';
                }
            }).catch(function(error){
                console.log(error);
                _this.$message({
                    message:'服务器错误',
                    type:'error'
                });
            });;
        },
        GetcustomInfo(pagesize,currentpage,name){       //获取客户列表信息
            var _this=this;
            var params={};
            if(name!=''){
                params={
                    pageSize:pagesize,
                    pageNo:currentpage,
                    agentId:_this.accountlist.agentid,
                    CustomName:name
                };
            }else{
                params={
                    pageSize:pagesize,
                    pageNo:currentpage,
                    agentId:_this.accountlist.agentid
                };
            }

            _this.$http.get('/Admin/AgentFinance/GetAgentCustomDetail',{
                params:params
            }).then(function(response){
                var info=response.data;
                if(info!=''||info!=null){
                    _this.infos.rechargecount=info.TodayRechargeCount;
                    _this.infos.consumed=info.TodayTotalConsumed;
                    _this.infos.account=info.RechargAccount;
                    _this.infos.rebateaccount=info.RebateAccount;
                    _this.infos.legalcustom=info.LegalCustom;
                    _this.accountlist.tabledata=info.AgentCustomList;
                    _this.accountlist.total=info.TotalCount;
                }else{
                    _this.infos.rechargecount='0元';
                    _this.infos.consumed='0元';
                    _this.infos.account='0元';
                    _this.infos.rebateaccount='0元';
                    _this.infos.legalcustom='0个';
                }
            }).catch(function(error){
                console.log(error);
                _this.$message({
                    message:'服务器错误',
                    type:'error'
                });
            });
        },
        GetRecords(currentpage,pagesize,startime,endtime){         //获取流水详情
            var  _this=this;
            var type='custom';
            var params={};
            if(_this.customwoter.btnclick==true){
                type='custom';
            }else if(_this.agentwoter.btnclick==true){
                type='agent';
            }
            if(startime==''||endtime==''){
                params={
                    pageSize:pagesize,
                    pageNo:currentpage,
                    agentId:_this.accountlist.agentid,
                    type:type
                };
            }else{
                params={
                    pageSize:pagesize,
                    pageNo:currentpage,
                    agentId:_this.accountlist.agentid,
                    type:type,
                    startTime:startime,
                    endTime:endtime
                };
            }

            _this.$http.get('/Admin/AgentFinance/GetAgentFinacneRecord',{
                params:params
            }).then(function(response){
                var info=response.data;
                if(info!=''||info!=null){
                    _this.customwoter.total=info.TotalCount;
                    _this.customwoter.tabledata=info.agentCustoms;
                }else{
                    _this.customwoter.total=0;
                    _this.customwoter.tabledata=[];
                }
            });
        },
        FundOperation(type,money,remark){                     //转入转出资金操作
            var _this=this;
            var datainfo={};
            var operationtype=0;
            var messages='';
            if(type=='in'){
                if(_this.modelin.selectfrom=='1'&&_this.modelin.selectto=='1'){
                    operationtype=0;
                }else if(_this.modelin.selectfrom=='2'&&_this.modelin.selectto=='1'){
                    operationtype=1;
                }
                datainfo={
                    AgentId: _this.accountlist.agentid,
                    CustomId: _this.accountlist.customid,
                    FundAmount: parseInt(money),
                    OperationType: operationtype,
                    MarkText: remark
                };
                messages='转入成功';
            }else if(type=='to'){
                if(_this.modelto.selectfrom=='1'&&_this.modelto.selectto=='1'){
                    operationtype=2;
                }else{
                    operationtype=2;
                }
                datainfo={
                    AgentId: _this.accountlist.agentid,
                    CustomId: _this.accountlist.customid,
                    FundAmount: parseInt(money),
                    OperationType: operationtype,
                    MarkText: remark
                };
                messages='转出成功';
            }

            _this.$http.post('/Admin/AgentFinance/FundOperation',datainfo).then(function(response){
                var info=response.data;
                if(info.status=='no'){
                    _this.$message({
                        message:info.info,
                        type:'warning'
                    });
                    _this.modelin.index=false;
                        _this.modelto.index=false;
                        _this.GetcustomInfo(_this.accountlist.pagesize,_this.accountlist.currentPage,'');
                }else if(info.status=='success'){
                    _this.$message({
                        message:messages,
                        type:'success'
                    });
                    setTimeout(function(){
                        _this.modelin.index=false;
                        _this.modelto.index=false;
                        _this.GetcustomInfo(_this.accountlist.pagesize,_this.accountlist.currentPage,'');
                    },1500);
                }
            });
        },
        customlists(){             //客户列表
            var _this=this;
            _this.accountlist.index=true;
            _this.customwoter.index=false;
            _this.agentwoter.index=false;
            _this.agentwoter.btnclick=false;
            _this.customwoter.btnclick=false;
        },
        customwolists(){             //客户财务流水
            var _this=this;
            _this.accountlist.index=false;
            _this.customwoter.index=true;
            _this.agentwoter.index=false;
            _this.agentwoter.btnclick=false;
            _this.customwoter.btnclick=true;
            var currentpage=_this.customwoter.currentPage;
            var pagesize=_this.customwoter.pagesize;
            _this.GetRecords(currentpage,pagesize,'','');
        },
        agentwolists(){              //代理商财务流水
            var _this=this;
            _this.accountlist.index=false;
            _this.customwoter.index=true;
            _this.agentwoter.index=false;
            _this.agentwoter.btnclick=true;
            _this.customwoter.btnclick=false;
            var currentpage=_this.customwoter.currentPage;
            var pagesize=_this.customwoter.pagesize;
            _this.GetRecords(currentpage,pagesize,'','');
        },
        search(type){                //搜索
            var _this=this;
            if(type=='name'){
                if(_this.accountlist.name==''){
                    _this.$message({
                        message:'请输入要进行搜索的公司名称',
                        type:'warning'
                    });
                    _this.$refs.sername.focus();
                    return;
                }else{
                    var currentpage=_this.accountlist.currentPage;
                    var pagesize=_this.accountlist.pagesize;
                    var name=_this.accountlist.name;
                    _this.GetcustomInfo(pagesize,currentpage,name);
                }
            }else if(type=='times'){
                if(_this.customwoter.time==''){
                    _this.$message({
                        message:'请选择要搜索的时间段',
                        type:'warning'
                    });
                    return;
                }else{
                    var currentpage=_this.customwoter.currentPage;
                    var pagesize=_this.customwoter.pagesize;
                    var startime=_this.customwoter.time[0];
                    var endtime=_this.customwoter.time[1];
                    _this.GetRecords(currentpage,pagesize,startime,endtime);
                }
            }
        },
        reset(type,lists){                //重置
            var _this=this;
            if(type=='name'&&lists=='cuslists'){
                _this.accountlist.name='';
                _this.$refs.sername.focus();
                var currentpage=_this.accountlist.currentPage;
                var pagesize=_this.accountlist.pagesize;
                _this.GetcustomInfo(pagesize,currentpage,'');
            }else if(type=='times'&&lists=='cuswoter'){
                _this.customwoter.time='';
                var currentpage=_this.customwoter.currentPage;
                var pagesize=_this.customwoter.pagesize;
                _this.GetRecords(currentpage,pagesize,'','');
            }else if(type=='times'&&lists=='agentwoter'){
                _this.customwoter.time='';
                var currentpage=_this.customwoter.currentPage;
                var pagesize=_this.customwoter.pagesize;
                _this.GetRecords(currentpage,pagesize,'','');
            }
        },
        flowmoney(row,type){         //资金操作
            var _this=this;
            _this.accountlist.customid=row.CustomId;
            _this.accountlist.moneyinfos=row.CashCount;
            if(type=='in'){
                _this.modelin.index=true;
                _this.modelin.money='';
                _this.modelin.selectfrom='1';
                _this.modelin.selectto='1';
                _this.modelin.remark='';
                if(_this.modelin.selectfrom=='1'){
                    _this.modelin.accountmoney=_this.infos.account;
                }else if(_this.modelin.selectfrom=='2'){
                    _this.modelin.accountmoney=_this.infos.rebateaccount;
                }
                
            }else if(type=='to'){
                _this.modelto.index=true;
                _this.modelto.money='';
                _this.modelto.selectfrom='1';
                _this.modelto.selectto='1';
                _this.modelto.remark='';
                _this.modelto.accountmoney=row.CashCount;
            }
        },
        moneyfromitem(){             //转入资金来源下拉变化
            var _this=this;
            if(_this.modelin.selectfrom=='1'){
                _this.modelin.accountmoney=_this.infos.account;
            }else if(_this.modelin.selectfrom=='2'){
                _this.modelin.accountmoney=_this.infos.rebateaccount;
            }
        },
        moneyupdate(type){                   //确认转入转出资金
            var _this=this;
            var regnum = /(^[1-9]([0-9]+)?(\.[0-9]{1,2})?$)|(^(0){1}$)|(^[0-9]\.[0-9]([0-9])?$)/;    //金额正则校验
            if(type=='in'){
                var money=_this.modelin.money;
                var accmoney=_this.modelin.accountmoney;
                if(money==''){
                    _this.$message({
                        message:'请输入要转入的金额',
                        type:'warning'
                    });
                    _this.$refs.inmoney.focus();
                    return;
                }else if(_this.modelin.selectfrom==''){
                    _this.$message({
                        message:'请选择转入金额来源',
                        type:'warning'
                    });
                    return;
                }else if(_this.modelin.selectto==''){
                    _this.$message({
                        message:'请选择转入金额目地',
                        type:'warning'
                    });
                    return;
                }else if(!regnum.test(parseInt(money))){
                    _this.$message({
                        message:'请输入正确的金额',
                        type:'warning'
                    });
                    _this.$refs.inmoney.focus();
                    return;
                }else if(parseInt(money)>parseInt(accmoney)){
                    _this.$message({
                        message:'转入金额超过剩余金额，请重新输入！',
                        type:'warning'
                    });
                    _this.$refs.inmoney.focus();
                    return;
                }else{
                    _this.FundOperation('in',parseFloat(money),_this.modelin.remark);
                }
            }else if(type=='to'){
                var money=_this.modelto.money;
                var accmoney=_this.modelto.accountmoney;
                if(money==''){
                    _this.$message({
                        message:'请输入要转入的金额',
                        type:'warning'
                    });
                    _this.$refs.outmoney.focus();
                    return;
                }else if(_this.modelto.selectfrom==''){
                    _this.$message({
                        message:'请选择转入金额来源',
                        type:'warning'
                    });
                    return;
                }else if(_this.modelto.selectto==''){
                    _this.$message({
                        message:'请选择转入金额目地',
                        type:'warning'
                    });
                    return;
                }else if(!regnum.test(parseInt(money))){
                    _this.$message({
                        message:'请输入正确的金额',
                        type:'warning'
                    });
                    _this.$refs.outmoney.focus();
                    return;
                }else if(parseInt(money)>parseInt(accmoney)){
                    _this.$message({
                        message:'转入金额超过剩余金额，请重新输入！',
                        type:'warning'
                    });
                    _this.$refs.outmoney.focus();
                    return;
                }else{
                    _this.FundOperation('to',parseFloat(money),_this.modelto.remark);
                }
            }
        },
        agentitemchange(){           //代理商下拉切换
            var _this=this;
            if(_this.accountlist.index==true){
                _this.GetcustomInfo(_this.accountlist.pagesize,_this.accountlist.currentPage,'');
            }else{
                _this.GetcustomInfo(_this.accountlist.pagesize,_this.accountlist.currentPage,'');
                _this.GetRecords(_this.customwoter.currentPage,_this.customwoter.pagesize,'','');
            }
            
        },
        wotertime(row){               //格式化日期
            if(row.RecordTime!=''||row.RecordTime!=null){
                var time=row.RecordTime.replace('T',' ');
                time=time.split('.')[0];
                row.RecordTime=time;
                return time;
            }
            
        },
        woterRemark(row){             //格式化备注
            if(row.MarkText==null||row.MarkText==''){
                row.MarkText='/';
            }else{
                return row.MarkText;
            }
        },
        customSizeChange(val) {    //客户列表显示条数
            this.accountlist.pagesize=val;
            var _this=this;
            _this.accountlist.currentPage=1;
            var currentpage=_this.accountlist.currentPage;
            var pagesize=_this.accountlist.pagesize;
            _this.GetcustomInfo(pagesize,currentpage,'');
        },
        customCurrentChange(val) {    //客户列表当前页改变
            var _this=this;
            _this.accountlist.currentPage=val;
            var currentpage=_this.accountlist.currentPage;
            var pagesize=_this.accountlist.pagesize;
            _this.GetcustomInfo(pagesize,currentpage,'');
        },
        woterSizeChange(val){           //财务流水当前显示数据条数
            var _this=this;
            _this.customwoter.pagesize=val;
            _this.customwoter.currentPage=1;
            var currentpage=_this.customwoter.currentPage;
            var pagesize=_this.customwoter.pagesize;
            _this.GetRecords(currentpage,pagesize,'','');
        },
        woterCurrentChange(val){          //财务流水当前页改变
            var _this=this;
            _this.customwoter.currentPage=val;
            var currentpage=_this.customwoter.currentPage;
            var pagesize=_this.customwoter.pagesize;
            _this.GetRecords(currentpage,pagesize,'','');
        }
    },
    created(){
        var _this=this;
        _this.GetAgentLists();
    },
  }

</script>

<style>
 
</style>

<template>
  <div class="cusindex">

    <!--其它证件遮罩层-->
    <transition name="fade">
      <div id="bg" v-show="index.otherbg">
        <div id="cont">
          <div class="box">
              <img :src="index.othersrc" alt="" id="img" class="bigimgother" />
              <p v-show="index.bgbtn">
                <el-button type="primary" class="prebtn" id="last" :disabled="index.predis" @click="predispic()">上一张</el-button>
                <el-button type="primary" class="nextbtn" id="next" :disabled="index.nextdis" @click="nextdispic()">下一张</el-button>
              </p>
          </div>
        </div>
        <span id="close" @click="closebtn"></span>
      </div>
    </transition>
    
    <!--遮罩-->
    <transition name="fade">
        <div class="wrap_img" @click="bgclick" v-show="index.bgif">
          <img :src="index.bgsrc1" :class="[this.index.bgimg?'bigimg':'bigimg1']">
          <img :src="index.bgsrc2" class="bigimg2">
        </div>
    </transition>

    <!--主页-->
    <div v-show="index.audit">
      <div class="divbtn">
        <el-row :gutter="0">
          <el-col :span="2" class="elcol">
            <el-button type="primary" :class="[this.index.onclick1?'onclick':'point']" @click="allbtn()">所有公司</el-button>
          </el-col>
          <el-col :span="2" class="elcol">
            <el-button type="primary" :class="[this.index.onclick2?'onclick':'point']" @click="waitbtn()">待审核</el-button>
          </el-col>
          <el-col :span="2" class="elcol">
            <el-button type="primary" :class="[this.index.onclick3?'onclick':'point']" @click="passbtn()">已通过</el-button>
          </el-col>
          <el-col :span="2" class="elcol">
            <el-button type="primary" :class="[this.index.onclick4?'onclick':'point']" @click="notpass()">未通过</el-button>
          </el-col>
        </el-row>
      </div>
      
      <div class="divsearch">
        <el-row>
          <el-col :span="2" class="eltag">
            <el-tag type="info" class="tag">公司名称：</el-tag>
          </el-col>
          <el-col :span="5" class="elinput" :offset="1">
              <el-input v-model="index.searchname"
                @keyup.enter.native="sername()"
                ref="sername" class="search" 
                placeholder="输入名称进行搜索" clearable>
              </el-input> 
          </el-col>
          <el-col :span="2" :offset="1">
            <el-button type="primary" @click="sername()">查询</el-button>
          </el-col>
          <el-col :span="2" :offset="1">
            <el-button type="primary" @click="reset('name')">重置</el-button>
          </el-col>
          <el-col :span="1" :offset="6"></el-col>
        </el-row>
        
      </div>

      <div class="searchtime">
        <el-row>
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
      </div>

      <div class="divtable">       <!--表格部分-->
        <el-row>
          <el-table border 
            :data="index.tabledata"
            :default-sort = "{prop: 'CustomRegTime', order: 'descending'}">
            <el-table-column
              header-align="center"
              align="center"
              prop="CompanyName"
              label="公司名称">
            </el-table-column>
            <el-table-column 
              header-align="center"
              align="center"
              prop="Status"
              label="状态">
            </el-table-column>
            <el-table-column 
              header-align="center"
              align="center"
              prop="ConfirmedTime"
              time-format="yyyy-MM-dd HH:mm:ss"
              sortable
              label="通过时间"
              :formatter="formattimes">
            </el-table-column>
            <el-table-column 
              header-align="center"
              align="center"
              prop="CustomRegTime"
              sortable
              label="注册时间"
              :formatter="formattime">
            </el-table-column>
            <el-table-column 
              header-align="center"
              align="center"
              prop="ConfirmedPerson"
              label="审核员">
            </el-table-column>
            <el-table-column
              header-align="center"
              align="center"
              prop="EstablishedType"
              label="开户类型"
              :formatter="estype">
            </el-table-column>
            <el-table-column 
              header-align="center"
              align="center"
              prop="EstablishedPerson"
              label="开户人">
            </el-table-column>
            <el-table-column
              header-align="center"
              align="center"
              label="操作"
              width="200px">
              <template slot-scope="scope">
                  <el-button type="primary" v-show="index.audits" @click="auditing(scope.$index,scope.row)">审核</el-button>
                  <el-button type="warning" @click="freeze(scope.$index,scope.row)">冻结</el-button>
              </template>
            </el-table-column>
          </el-table>
        </el-row>

        <el-row>
          <el-pagination class="pagin" v-if="index.pagin"
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
    
    <!--客户审核部分-->
    <div v-show='index.cusaudit'>
      <div class="divinfo">
        <el-row>
          <el-col :span="3" :offset="5" class="divminbtn">
            <el-button type="info" class="minbtn">客户主页</el-button>
          </el-col>
          <el-col :span="9">
            <el-input class="inputinfo" v-model="toaudit.cuinfo" readonly="readonly"></el-input>
          </el-col>
        </el-row>
      </div>

      <div class="divicp">
        <el-row>
          <el-col :span="3" :offset="5" class="divminbtn">
            <el-button type="info" class="minbtn">ICP证书</el-button>
          </el-col>
          <el-col :span="6" class="elinput">
            <el-input :class="[this.toaudit.ificp?'inputone':'inputtwo']" :mouseover='lookicp' v-model="toaudit.icpinput" readonly="readonly"></el-input>
            <el-input v-model="toaudit.icpinput1" type="hidden"></el-input>
          </el-col>
          <el-col :span="2" :offset="1">
            <el-button type="primary" class="lookbtn" :disabled="toaudit.icpbtn" @click="lookicp">查看</el-button>
          </el-col>
        </el-row> 
      </div>

      <div class="divicp">
        <el-row>
          <el-col :span="3" :offset="5" class="divminbtn">
            <el-button type="info" class="minbtn">营业执照</el-button>
          </el-col>
          <el-col :span="6" class="elinput">
            <el-input :class="[this.toaudit.ifbus?'inputone':'inputtwo']" v-model="toaudit.businput" readonly="readonly"></el-input>
            <el-input v-model="toaudit.businput1" type="hidden"></el-input>
          </el-col>
          <el-col :span="2" :offset="1">
            <el-button type="primary" class="lookbtn" :disabled="toaudit.busbtn" @click="lookbus">查看</el-button>
          </el-col>
        </el-row>
      </div>

      <div class="divicp">
        <el-row>
          <el-col :span="3" :offset="5" class="divminbtn">
            <el-button type="info" class="minbtn">身份证件</el-button>
          </el-col>
          <el-col :span="6" class="elinput">
            <el-input :class="[this.toaudit.ifidty?'inputone':'inputtwo']" v-model="toaudit.idtyinput" readonly="readonly"></el-input>
            <el-input type="hidden"></el-input>
          </el-col>
          <el-col :span="2" :offset="1">
            <el-button type="primary" class="lookbtn" :disabled="toaudit.idtybtn" @click="lookidty">查看</el-button>
          </el-col>
        </el-row>
      </div>

      <div class="divicp">
        <el-row>
          <el-col :span="3" :offset="5" class="divminbtn">
            <el-button type="info" class="minbtn">其它证件</el-button>
          </el-col>
          <el-col :span="6">
            <el-input :class="[this.toaudit.ifother?'inputone':'inputtwo']" v-model="toaudit.otherinput" readonly="readonly"></el-input>
            <el-input type="hidden"></el-input>
          </el-col>
          <el-col :span="2" :offset="1">
            <el-button type="primary" class="lookbtn" :disabled="toaudit.otherbtn" @click="lookother">查看</el-button>
          </el-col>
        </el-row>
      </div>

      <div class="divpass">
        <el-row>
          <el-col :span="3" :offset="5">
            <el-button type="info" class="minbtn">审核状态</el-button>
          </el-col>
          <el-col :span="8">
            <el-col :span="12">
              <el-radio class="elrad" v-model="toaudit.rastate" label="1">通过</el-radio>
            </el-col>
            <el-col :span="12">
              <el-radio class="elrad2" v-model="toaudit.rastate" label="4">未通过</el-radio>
            </el-col>
          </el-col>
        </el-row>
        
      </div>

      <div class="divnotpass">
        <el-row>
          <el-col :span="3" :offset="5" class="divminbtn">
            <el-button type="info" class="minbtn">未通过原因</el-button>
          </el-col>
          <el-col :span="9">
            <el-input
              type="textarea"
              :autosize="{ minRows: 7}"
              placeholder="请输入未通过的原因"
              v-model="toaudit.reason" clearable>
            </el-input>
          </el-col>
        </el-row>
      </div>

      <div class="divdaili">
        <el-row>
          <el-col :span="3" :offset="5">
            <el-input type="hidden"></el-input>
          </el-col>
          <el-col :span="9" class="radopen">
            <el-col :span="9">
              <el-radio v-model="toaudit.raopen" label="0" :click="raclick1()">自主开户</el-radio>
            </el-col>
            <el-col :span="9">
              <el-radio v-model="toaudit.raopen" label="1" :click="raclick1()">代理商</el-radio>
            </el-col>
            <el-col :span="6">
              <el-radio v-model="toaudit.raopen" label="2" :click="raclick1()">销售</el-radio>
            </el-col>
          </el-col>
        </el-row>
      </div>

      <div class="divsubjection">
        <el-row>
          <el-col :span="4" :offset="5" class="divminbtn">
            <el-button type="info" class="minbtn">所属代理商或销售</el-button>
          </el-col>
          <el-col :span="8">
            <el-select class="agent" :disabled="toaudit.seladis" v-model="toaudit.agent" clearable :placeholder="toaudit.plahoder" v-if="toaudit.ifselect1">
              <!-- <el-option>
              </el-option> -->
            </el-select>

          <el-select class="market" v-model="toaudit.agent" clearable :placeholder="toaudit.plahoder" v-if="toaudit.ifselect2">
              <el-option
              v-for="item in toaudit.agentlist"
              :key="item.value"
              :label="item.text"
              :value="item.value">
              </el-option>
            </el-select>

            <el-select class="market" v-model="toaudit.market" clearable :placeholder="toaudit.plahoder1" v-if="toaudit.ifselect3">
              <el-option
              v-for="item in toaudit.marketlist"
              :key="item.value"
              :label="item.text"
              :value="item.value">
              </el-option>
            </el-select>
          </el-col>
        </el-row>
        
      </div>

      <div class="divsure">
        <el-row>
          <el-col :span="4" :offset="3">
            <el-input type="hidden"></el-input>
          </el-col>
          <el-col :span="9">
            <el-col :span="6">
              <el-button type="primary" class="surebtn" @click="btnsure">确认</el-button>
            </el-col>
            <el-col :span="12"></el-col>
            <el-col :span="6" :offset="12" class="rebackbtn">
              <el-button type="info" @click="btnreback">取消</el-button>
            </el-col>
          </el-col>
        </el-row>
      </div>
    </div>
  </div>
</template>

<script>
  import '../css/customs.less'
  import '../js/axios.js'
  export default {
    name: "Customs",
    data () {
      return {
        index:{
          onclick1:true,      //通过点击改变按钮样式
          onclick2:false,
          onclick3:false,
          onclick4:false,
          otherbg:false,        //其它证件遮罩v-if
          bgbtn:false,          //上一张下一张的v-if
          bgif:false,           //遮罩
          bgimg:true,          //其它身份证件css三目运算符
          predis:'',           //上一张disabled
          nextdis:'',           //下一张disabled
          searchname:'',       //搜索框
          time:'',             //时间选择
          audit:true,          //广告审核v-if
          cusaudit:false,
          bgsrc1:'',             //遮罩图1
          bgsrc2:'',            //遮罩图2
          othersrc:'',         //其它证件遮罩图片
          tabledata:[],            //表格数据
          tabledatas:[], 
          status:0,              //状态查询
          currentPage:1,            //默认第几页
          pagesize:8,            //每页数量
          total:0,              //数据总数
          pagin:true,            //分页
          picindex:0,
          audits:true,            //审核按钮v-show
        },
        toaudit:{
          cuinfo:'',           //客户主页
          icpinput:'未上传',         //ICP证书
          icpinput1:'',
          businput:'未上传',         //营业执照
          businput1:'',
          idtyinput:'未上传',        //身份证件
          idtyinput1:[],
          otherinput:'未上传',       //其它证件
          otherinput1:[],
          rastate:'1',           //审核单选
          raopen:'0',             //开户单选
          agentlist:[],              //代理商下拉列表
          marketlist:[],           //销售下拉列表
          ifselect1:true,          //下拉 v-if
          ifselect2:false,
          ifselect3:false,
          agent:'',                //所选代理商
          market:'',               //所选销售
          ificp:false,              //icp  v-if
          ifbus:false,              //营业执照v-if
          ifidty:false,             //身份证件 v-if
          ifother:false,            //其它证件 v-if
          icpbtn:false,             //icp查看按钮
          busbtn:false,             //营业执照查看按钮
          idtybtn:false,            //身份证件查看按钮
          otherbtn:false,           //其它证件查看按钮
          reason:'',             //未通过的原因
          plahoder:'',           //下拉列表的placeholder
          plahoder1:'',
          seladis:true,            //下拉列表的disable
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
      GetCustomlist(currentpage,pagesize,state,name,stime,endtime){     //通过传参来分页查询
        var _this=this;
        var params={};
        _this.index.tabledata=[];
        if(name==''&&stime==''&&endtime==''){
          params={
            pageSize:pagesize,
            pageNo:currentpage,
            state:state
          };
        }else if(name!=''&&stime==''&&endtime==''){
          params={
            pageSize:pagesize,
            pageNo:currentpage,
            state:state,
            companyName:name
          };
        }else if(name==''&&stime!=''&&endtime!=''){
          params={
            pageSize:pagesize,
            pageNo:currentpage,
            state:state,
            sTime:stime,
            eTime:endtime
          };
        }
        this.$http.get('/Admin/Customs/GetCustomByPage',{
          params:params
        }).then(function(response){
          var info=response.data;
          if(info!=''||info!=null){
            _this.index.total=info.TotalNum;
            _this.index.tabledata=info.CustomList;
          }else{
            _this.index.total=0;
            _this.index.tabledata=[];
          }

          if(_this.index.total<=0){
            _this.index.pagin=false;
          }else{
            _this.index.pagin=true;
          }
          
        }).catch(function(error) {
          console.log(error);
          _this.$message({
            message:'服务器错误',
            type:'error'
          });
        });
      },
      GetAgtOpsList(role){                    //获取代理商销售列表
        var _this=this;
        if(role==3){
          this.$http.post('/Admin/Customs/GetAgtOpsLists',{
            role:role
          }).then(function(resonpse){
            if(resonpse.data.status=='ok'){
              if(resonpse.data.data.length>0){
                for (let index = 0; index < resonpse.data.data.length; index++) {
                  _this.toaudit.agentlist.push(JSON.parse(resonpse.data.data[index]));
                }
                _this.toaudit.plahoder='请选择代理商';
              }else{
                _this.toaudit.agentlist=[];
                _this.toaudit.plahoder='暂无代理商';
              }
              
            }else{
              _this.toaudit.agentlist=[];
              _this.toaudit.plahoder='服务器繁忙';
            }
          }).catch(function(error){
            console.log(error);
            _this.$message({
              message:'服务器错误',
              type:'error'
            });
          });
        }else{
          this.$http.post('/Admin/Customs/GetAgtOpsLists',{
            role:role
          }).then(function(resonpse){
            if(resonpse.data.status=='ok'){
              if(resonpse.data.data.length>0){
                for (let index = 0; index < resonpse.data.data.length; index++) {
                  _this.toaudit.marketlist.push(JSON.parse(resonpse.data.data[index]));
                }
                _this.toaudit.plahoder1='请选择销售';
              }else{
                _this.toaudit.agentlist=[];
                _this.toaudit.plahoder1='暂无销售';
              }
            }else{
              _this.toaudit.marketlist=[];
              _this.toaudit.plahoder1='服务器繁忙';
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
      allbtn(){    //所有公司按钮
        var _this=this;
        this.index.onclick1=true;   //改变按钮样式
        this.index.onclick2=false;
        this.index.onclick3=false;
        this.index.onclick4=false;
        this.index.status=0;
        var currentpage=_this.index.currentPage;
        var pagesize=_this.index.pagesize;
        var state=_this.index.status;
        _this.GetCustomlist(currentpage,pagesize,state,'','','');
      },
      waitbtn(){    //待审核按钮
        var _this=this;
        this.index.onclick1=false;    //改变按钮样式
        this.index.onclick2=true;
        this.index.onclick3=false;
        this.index.onclick4=false;
        this.index.status=2;
        
        var currentpage=_this.index.currentPage;
        var pagesize=_this.index.pagesize;
        var state=_this.index.status;
        _this.GetCustomlist(currentpage,pagesize,state,'','','');
      },
      passbtn(){    //已通过按钮
        var _this=this;
        this.index.onclick1=false;    //改变按钮样式
        this.index.onclick2=false;
        this.index.onclick3=true;
        this.index.onclick4=false;
        this.index.status=1;
        
        var currentpage=_this.index.currentPage;
        var pagesize=_this.index.pagesize;
        var state=_this.index.status;
        _this.GetCustomlist(currentpage,pagesize,state,'','','');
      },
      notpass(){    //未通过按钮
        var _this=this;
        this.index.onclick1=false;    //改变按钮样式
        this.index.onclick2=false;
        this.index.onclick3=false;
        this.index.onclick4=true;
        this.index.status=4;
        var currentpage=_this.index.currentPage;
        var pagesize=_this.index.pagesize;
        var state=_this.index.status;
        _this.GetCustomlist(currentpage,pagesize,state,'','','');
      },
      sername(){      //查询名字按钮
        var _this=this;
        if(_this.index.searchname==''){
          this.$message({
						message: '请输入要查询的公司名称',
						type: 'warning'
          });
          this.$refs.sername.focus();
          return;
        }else{
          var currentpage=_this.index.currentPage;
          var pagesize=_this.index.pagesize;
          var state=_this.index.status;
          var name=_this.index.searchname;
          _this.GetCustomlist(currentpage,pagesize,state,name,'','');
          _this.index.searchname='';
        }
      },
      sertime(){       //查询时间按钮
        var _this=this;
        if(this.index.time==''){
          this.$message({
						message: '请输入要查询的时间段',
						type: 'warning'
          });
          return;
        }else{
          var currentpage=_this.index.currentPage;
          var pagesize=_this.index.pagesize;
          var state=_this.index.status;
          var stime=_this.index.time[0];
          var endtime=_this.index.time[1];
          _this.GetCustomlist(currentpage,pagesize,state,'',stime,endtime);
        }
      },
      formattimes(row){                 //时间格式化
        if(row.ConfirmedTime!=''||row.ConfirmedTime!=null){
          var time=row.ConfirmedTime.replace('T',' ');
          time=time.split('.')[0];
          row.ConfirmedTime=time;
          return time;
        }
      },
      formattime(row){                 //时间格式化
        if(row.CustomRegTime!=''||row.CustomRegTime!=null){
          var time=row.CustomRegTime.replace('T',' ');
          time=time.split('.')[0];
          row.CustomRegTime=time;
          return time;
        }
      },
      estype(row){                      //开户类型格式化
        switch (row.EstablishedType) {
          case 0:
            return '自主开户';
            break;
          case 1:
            return '代理商';
            break;
          case 2:
            return '销售';
            break;
          default:
            break;
        }
      },
      reset(type){                         //重置
        var _this=this;
        var state=0;
        if(_this.index.onclick1==true){
          state=0;
        }else if(_this.index.onclick2==true){
          state=2;
        }else if(_this.index.onclick3==true){
          state=1;
        }else if(_this.index.onclick4==true){
          state=4;
        }
        if(type=='name'){
          _this.index.searchname='';
          _this.GetCustomlist(_this.index.currentPage,_this.index.pagesize,state,'','','');
        }else{
          _this.index.time='';
          _this.GetCustomlist(_this.index.currentPage,_this.index.pagesize,state,'','','');
        }
      },
      lookicp(){      //查看ICP证书
        this.index.bgif=true;
        this.index.bgsrc1=this.toaudit.icpinput1;
        this.index.bgsrc2='';
        this.index.bgimg=true;
      },
      lookbus(){      //查看营业执照
        this.index.bgif=true;
        this.index.bgsrc1=this.toaudit.businput1;
        this.index.bgsrc2='';
        this.index.bgimg=true;
      },
      lookidty(){      //查看身份证件
        this.index.bgif=true;
        this.index.bgsrc1=this.toaudit.idtyinput1[0];
        this.index.bgsrc2=this.toaudit.idtyinput1[1];
        this.index.bgimg=false;
      },
      lookother(){       //查看其它证件
        this.index.picindex=0;
        this.index.otherbg=true;
        this.index.bgif=false;
        if(this.toaudit.otherinput1.length==1){
          this.index.bgbtn=false;
          this.index.predis=true;
          this.index.nextdis=true;
          this.index.othersrc=this.toaudit.otherinput1[this.index.picindex];
        }else{
          this.index.bgbtn=true;
          this.index.predis=false;
          this.index.nextdis=false;
          this.index.othersrc=this.toaudit.otherinput1[this.index.picindex];
        }
      },
      predispic(){               //上一张
        this.index.nextdis=false;
        if(this.index.picindex==0){
          this.$message({
            message:'这已经是第一张了哦~',
            type:'warning'
          });
          this.index.predis=true;
        }else{
          this.index.picindex--;
          this.index.othersrc=this.toaudit.otherinput1[this.index.picindex];
        }
      },
      nextdispic(){                //下一张
        this.index.predis=false;
        if(this.index.picindex>=(this.toaudit.otherinput1.length-1)){
          this.$message({
            message:'这已经是最后一张了哦~',
            type:'warning'
          });
          this.index.nextdis=true;
        }else{
          this.index.picindex=this.index.picindex+1;
          this.index.othersrc=this.toaudit.otherinput1[this.index.picindex];
        }
      },
      btnsure(){        //确认审核按钮
        var _this=this;
        var open='';   //开户所属
        if(_this.toaudit.rastate=='4'){
          if(_this.toaudit.reason==''){
            _this.$message({
              message:'请填写未通过的原因',
              type:'warning'
            });
            return;
          }
        }
        if(_this.toaudit.raopen=='1'){
          if(_this.toaudit.agent==''){
            _this.$message({
              message:'请选择所属代理商',
              type:'warning'
            });
            return;
          }
          open=_this.toaudit.agent;
        }else if(_this.toaudit.raopen=='2'){
          if(_this.toaudit.market==''){
            _this.$message({
              message:'请选择所属销售',
              type:'warning'
            });
            return;
          }
          open=_this.toaudit.market;
        }else{
          open='';
        }
      },
      btnreback(){       //取消按钮
        this.index.audit=true;
        this.index.cusaudit=false;
      },
      raclick1(type){         //自主开户单选
        if(this.toaudit.raopen=='0'){
          this.toaudit.plahoder='这是代理商或者销售选项';
          this.toaudit.ifselect1=true;
          this.toaudit.ifselect2=false;
          this.toaudit.ifselect3=false;
          this.toaudit.seladis='disabled';
        }else if(this.toaudit.raopen=='1'){
          this.toaudit.ifselect1=false;
          this.toaudit.ifselect2=true;
          this.toaudit.ifselect3=false;
          this.toaudit.seladis="";
          this.toaudit.plahoder='请选择代理商';
        }else if(this.toaudit.raopen=='2'){
          this.toaudit.ifselect1=false;
          this.toaudit.ifselect2=false;
          this.toaudit.ifselect3=true;
          this.toaudit.plahoder='请选择销售';
          this.toaudit.seladis='';
        }
        
      },
      bgclick(){           //遮罩
        this.index.bgif=false;
      },
      closebtn(){          //其它遮罩
        this.index.otherbg=false; 
      },
      auditing(index,row){                //审核按钮
        this.index.bgsrc1='';
        this.index.bgsrc2='';
        this.index.otherbg='';
        this.index.audit=false;
        this.index.cusaudit=true;
        var _this=this;
        _this.$http.post('/Admin/Customs/CheckInfo',{
          CustomId:row.CustomId
        }).then(function(response){
          var info=response.data;
          if(info.HomePage==null||info.HomePage==''){
            _this.toaudit.cuinfo='请先上传资质';
          }else{
            _this.toaudit.cuinfo=info.HomePage;
          }
          if(info.ICPAuthStr==null||info.ICPAuthStr==''){
            _this.toaudit.icpinput='未上传';
            _this.toaudit.ificp=false;
            _this.toaudit.icpbtn=true;
          }else{
            _this.toaudit.icpinput='已上传';
            _this.toaudit.ificp=true;
            _this.toaudit.icpbtn=false;
            _this.toaudit.icpinput1=info.ICPAuthStr;
          }
          if(info.BusinessAuthStr==null||info.BusinessAuthStr==''){
            _this.toaudit.businput='未上传';
            _this.toaudit.ifbus=false;
            _this.toaudit.busbtn=true;
          }else{
            _this.toaudit.businput='已上传';
            _this.toaudit.ifbus=true;
            _this.toaudit.busbtn=false;
            _this.toaudit.businput1=info.BusinessAuthStr;
          }
          if(info.CardAuthStrList.length==0||info.CardAuthStrList==''){
            _this.toaudit.idtyinput='未上传';
            _this.toaudit.ifidty=false;
            _this.toaudit.idtybtn=true;
          }else{
            _this.toaudit.idtyinput='已上传';
            _this.toaudit.ifidty=true;
            _this.toaudit.idtybtn=false;
            _this.toaudit.idtyinput1=info.CardAuthStrList;
          }
          if(info.OtherAuthStr.length==0||info.OtherAuthStr==''){
            _this.toaudit.otherinput='未上传';
            _this.toaudit.ifother=false;
            _this.toaudit.otherbtn=true;
          }else{
            _this.toaudit.otherinput='已上传';
            _this.toaudit.ifother=true;
            _this.toaudit.otherbtn=false;
            _this.toaudit.otherinput1=info.OtherAuthStr;
          }
          if(info.Status==1){
            _this.toaudit.rastate='1';
          }else if(info.Status==4){
            _this.toaudit.rastate='4';
          }else{
            _this.toaudit.rastate='1';
          }

          if(info.NoPassReason==''||info.NoPassReason==null){
            _this.toaudit.reason='';
          }else{
            _this.toaudit.reason=info.NoPassReason;
          }

          if(info.EstablishType==0){
            _this.toaudit.raopen='0';
            _this.toaudit.ifselect2=true;
            _this.toaudit.ifselect2=false;
            _this.toaudit.ifselect2=false;
          }else if(info.EstablishType==1){
            _this.toaudit.raopen='1';
            _this.toaudit.ifselect2=false;
            _this.toaudit.ifselect2=true;
            _this.toaudit.ifselect2=false;
          }else{
            _this.toaudit.raopen='2';
            _this.toaudit.ifselect2=false;
            _this.toaudit.ifselect2=false;
            _this.toaudit.ifselect2=true;
          }

          if(info.AgentSaleStr==''||info.AgentSaleStr==null){
            _this.toaudit.market='';
          }else{
            _this.toaudit.market=info.AgentSaleStr;
          }
        }).catch(function(error){
          console.log(error);
          _this.$message({
            message:'服务器错误',
            type:'error'
          });
        });
      },
      freeze(index,row){                   //冻结
        var _this=this;
        this.$confirm('此操作将永久冻结该用户, 是否继续?', '提示', {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning'
        }).then(() => {
          _this.$http.post('/Admin/Customs/FreezeCompany',{
            CustomId:row.CustomId
          }).then(function(response){
            _this.$message({
              type: 'warning',
              message: response.data
            });
          }).catch(function(error){
            console.log(error);
            _this.$message({
              message:'服务器错误',
              type:'error'
            });
          });
        }).catch(() => {
          this.$message({
            type: 'info',
            message: '已取消'
          });          
        });
      },
      handleSizeChange(val) {    //分页功能
        this.index.pagesize=val;
        var _this=this;
        _this.index.currentPage=1;
        var currentpage=_this.index.currentPage;
        var pagesize=_this.index.pagesize;
        var state=_this.index.status;
        _this.GetCustomlist(currentpage,pagesize,state,'','','');
      },
      handleCurrentChange(val) {    //当前页改变
        var _this=this;
        _this.index.currentPage=val;
        var currentpage=_this.index.currentPage;
        var pagesize=_this.index.pagesize;
        var state=_this.index.status;
        _this.GetCustomlist(currentpage,pagesize,state,'','','');
      },
    },
    mounted() {
      var _this=this;
      if(_this.toaudit.icpinput=='未上传'){
        _this.toaudit.icpbtn=true;
      }
      if(_this.toaudit.businput=='未上传'){
        _this.toaudit.busbtn=true;
      }
      if(_this.toaudit.idtyinput=='未上传'){
        _this.toaudit.idtybtn=true;
      }
      if(_this.toaudit.otherinput=='未上传'){
        _this.toaudit.otherbtn=true;
      }
    },
    created(){
      var _this=this;
      _this.GetAgtOpsList(3);
      _this.GetAgtOpsList(4);

      var currentpage=_this.index.currentPage;
      var pagesize=_this.index.pagesize;
      var state=_this.index.status;
      _this.GetCustomlist(currentpage,pagesize,state,'','','');
    }
  }
</script>

<style scoped>
  
</style>

<template>
  <div class="adsmg">
    <!--其它证件遮罩层-->
    <transition  name="fade">
      <div id="bg" v-show="index.otherbg">
        <div id="cont">
          <div class="box">
              <img :src="index.othersrc" alt="" id="img" class="bigimgother" />
              <p v-show="index.bgbtn">
                <el-button type="primary" class="prebtn" id="last" @click="predispic()" :disable="index.predis">上一张</el-button>
                <el-button type="primary" class="nextbtn" id="next" @click="nextdispic()" :disable="index.nextdis">下一张</el-button>
              </p>
          </div>
        </div>
        <span id="close" @click="closebtn()"></span>
      </div>
    </transition>
    
    <!--遮罩-->
    <transition  name="fade">
      <div class="wrap_img transition-box" @click="bgclick()" v-if="index.bgif">
        <img :src="index.bgsrc1" :class="[this.index.bgimg?'bigimg':'bigimg1']">
        <img :src="index.bgsrc2" class="bigimg2">
      </div>
    </transition>

    <!--主页-->
    <div v-show="index.audit">
      <div class="adsbtn">
        <el-row :gutter="0">
          <el-col :span="2" class="elcol">
            <el-button type="primary" :class="[this.index.onclick1?'onclick':'point']" @click="waitbtn()">待审核</el-button>
          </el-col>
          <el-col :span="2" class="elcol">
            <el-button type="primary" :class="[this.index.onclick2?'onclick':'point']" @click="passbtn()">已通过</el-button>
          </el-col>
          <el-col :span="2" class="elcol">
            <el-button type="primary" :class="[this.index.onclick3?'onclick':'point']" @click="notpass()">未通过</el-button>
          </el-col>
        </el-row>
      </div>

      <div class="adsearch">
        <el-row>
          <el-col :span='2' class="searsel">
            <el-select v-model="index.select" placeholder="请选择" @change="selects()">
              <el-option
                v-for="item in index.search"
                :key="item.value"
                :label="item.name"
                :value="item.value">
              </el-option>
            </el-select>
          </el-col>
          <el-col :span="6" class="elinput" :offset="1">
              <el-input v-model="index.searchname" ref="sername" class="search"
                :placeholder="index.placeholder" @keyup.enter.native="sername()" clearable>
              </el-input> 
          </el-col>
          <el-col :span="2" :offset="1">
            <el-button type="primary" @click="sername()">查询</el-button>
          </el-col>
          <el-col :span="2" :offset="1">
            <el-button type="primary" @click="reset()">重置</el-button>
          </el-col>
        </el-row>
      </div>

      <div class="adstable">       <!--表格部分-->
        <el-row>
          <el-table stripe border 
                :data="index.tabledata"
                :default-sort = "{prop: 'CompanyName', order: 'descending'}">
                <el-table-column
                  header-align="center"
                  align="center"
                  prop="AdId"
                  label="ID"
                  >
                </el-table-column>
                <el-table-column
                  header-align="center"
                  align="center"
                  prop="CompanyName"
                  sortable
                  label="公司名字">
                </el-table-column>
                <el-table-column
                  header-align="center"
                  align="center"
                  prop="PayPlatform"
                  label="平台">
                </el-table-column>
                <el-table-column
                  header-align="center"
                  align="center"
                  prop="AdsListName"
                  label="广告系列名">
                </el-table-column>
                <el-table-column
                  header-align="center"
                  align="center"
                  prop="AdName"
                  label="广告名">
                </el-table-column>
                <el-table-column
                  header-align="center"
                  align="center"
                  prop="AdConfirmedState"
                  label="状态">
                </el-table-column>
                <el-table-column
                  header-align="center"
                  align="center"
                  label="操作"
                  width="200px">
                  <template slot-scope="scope">
                    <el-button type="primary" v-show="index.auditing" @click="Auditing(scope.row)" size="small">审核</el-button>
                    <el-tooltip class="item" effect="dark" content="此功能暂未开放！" placement="top">
                      <el-button type="warning" @click="freeze(scope.row)" size="small">冻结</el-button>
                    </el-tooltip>
                  </template>
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

    <!--广告审核部分-->
    <div v-show="toaudit.audit">
      <div class="adsinfo">
        <el-row>
          <el-col :span="3" :offset="5" class="divminbtn">
            <el-button type="info" class="minbtn">客户主页</el-button>
          </el-col>
          <el-col :span="9">
            <el-input class="inputinfo" v-model="toaudit.cuinfo" readonly="readonly"></el-input>
          </el-col>
        </el-row>  
      </div>

      <div class="adsicp">
        <el-row>
          <el-col :span="3" :offset="5" class="divminbtn">
            <el-button type="info" class="minbtn">ICP证书</el-button>
          </el-col>
          <el-col :span="6" class="elinput">
            <el-input :class="[this.toaudit.ificp?'inputone':'inputtwo']" v-model="toaudit.icpinput" readonly="readonly"></el-input>
            <!-- <el-input class="inputtwo" v-model="toaudit.icpinput" readonly="readonly" v-else></el-input> -->
          </el-col>
          <el-col :span="2" :offset="1">
            <el-button type="primary" class="lookbtn" :disabled="toaudit.icpbtn" @click="lookicp()">查看</el-button>
          </el-col>
        </el-row>
      </div>

      <div class="adsicp">
        <el-row>
          <el-col :span="3" :offset="5" class="divminbtn">
            <el-button type="info" class="minbtn">营业执照</el-button>
          </el-col>
          <el-col :span="6" class="elinput">
            <el-input :class="[this.toaudit.ifbus?'inputone':'inputtwo']" v-model="toaudit.businput" readonly="readonly"></el-input>
            <!-- <el-input class="inputtwo" v-model="toaudit.businput" readonly="readonly" v-else></el-input> -->
          </el-col>
          <el-col :span="2" :offset="1">
            <el-button type="primary" class="lookbtn" :disabled="toaudit.busbtn" @click="lookbus()">查看</el-button>
          </el-col>
        </el-row>
      </div>

      <div class="adsicp">
        <el-row>
          <el-col :span="3" :offset="5" class="divminbtn">
            <el-button type="info" class="minbtn">身份证件</el-button>
          </el-col>
          <el-col :span="6" class="elinput">
            <el-input :class="[this.toaudit.ifidty?'inputone':'inputtwo']" v-model="toaudit.idtyinput" readonly="readonly"></el-input>
            <!-- <el-input class="inputtwo" v-model="toaudit.idtyinput" readonly="readonly" v-else></el-input> -->
          </el-col>
          <el-col :span="2" :offset="1">
            <el-button type="primary" class="lookbtn" :disabled="toaudit.idtybtn" @click="lookidty()">查看</el-button>
          </el-col>
        </el-row>
      </div>

      <div class="adsicp">
        <el-row>
          <el-col :span="3" :offset="5" class="divminbtn">
            <el-button type="info" class="minbtn">其它证件</el-button>
          </el-col>
          <el-col :span="6">
            <el-input :class="[this.toaudit.ifother?'inputone':'inputtwo']" v-model="toaudit.otherinput" readonly="readonly"></el-input>
            <!-- <el-input class="inputtwo" v-model="toaudit.otherinput" readonly="readonly" v-else></el-input> -->
          </el-col>
          <el-col :span="2" :offset="1">
            <el-button type="primary" class="lookbtn" :disabled="toaudit.otherbtn" @click="lookother()">查看</el-button>
          </el-col>
        </el-row>
      </div>

      <div class="adsspread">
        <el-row>
          <el-col :span="3" :offset="5" class="divminbtn">
            <el-button type="info" class="minbtn">推广类型</el-button>
          </el-col>
          <el-col :span="9" class="radopen">
            <el-col :span="7" :offset="1">
              <el-radio v-model="toaudit.raopen" disabled label="0">网站链接</el-radio>
            </el-col>
            <el-col :span="7">
              <el-radio v-model="toaudit.raopen" disabled label="1">应用宝下载</el-radio>
            </el-col>
            <el-col :span="8" :offset="1">
              <el-radio v-model="toaudit.raopen" disabled label="2">IOS应用下载</el-radio>
            </el-col>
          </el-col>
        </el-row>
      </div>

      <div class="adsinfo">
        <el-row>
          <el-col :span="3" :offset="5" class="divminbtn">
            <el-button type="info" class="minbtn">广告标题</el-button>
          </el-col>
          <el-col :span="9">
            <el-input class="inputinfo" v-model="toaudit.title" readonly="readonly"></el-input>
          </el-col>
        </el-row>
      </div>

      <div class="adstype">
        <el-row>
          <el-col :span="3" :offset="5" class="divminbtn">
            <el-button type="info" class="minbtn">投放类型</el-button>
          </el-col>
          <el-col :span="9">
            <el-input class="inputinfo" v-model="toaudit.playtype" readonly="readonly"></el-input>
          </el-col>
        </el-row>
      </div>

      <div class="adlists">
        <el-row>
          <el-col :span="3" :offset="5" class="divminbtn">
            <el-button type="info" class="minbtn">广告品类</el-button>
          </el-col>
          <el-col :span="4">
            <el-select v-model="toaudit.selectitem1" placeholder="请选择" disabled>
              <el-option
                v-for="item in toaudit.selectitem1s"
                :key="item.Data.type_id"
                :label="item.Data.type_name"
                :value="item.Data.type_id">
              </el-option>
            </el-select>
          </el-col>
          <el-col :span="4" :offset="1">
            <el-select v-model="toaudit.selectitem2" placeholder="请选择" disabled>
              <el-option
                v-for="item in toaudit.selectitem2s"
                :key="item.Data.id"
                :label="item.Data.name"
                :value="item.Data.id">
              </el-option>
            </el-select>
          </el-col>
        </el-row>
      </div>

      <div class="adsicp">
        <el-row>
          <el-col :span="3" :offset="5" class="divminbtn">
            <el-button type="info" class="minbtn">广告链接</el-button>
          </el-col>
          <el-col :span="6" class="inputurl">
            <el-input v-model="toaudit.url" readonly="readonly"></el-input>
          </el-col>
          <el-col :span="2" :offset="1">
            <el-button type="primary" class="lookbtn" :disabled='toaudit.urlbtn' @click="openurl()">打开</el-button>
          </el-col>
        </el-row>
      </div>

      <div class="adsicp">
        <el-row>
          <el-col :span="3" :offset="5" class="divminbtn">
            <el-button type="info" class="minbtn">广告素材</el-button>
          </el-col>
          <el-col :span="6" class="divmat">
            <el-input :class="[this.toaudit.ifpic?'inputmat':'inputone']" id="material" v-model="toaudit.material" readonly="readonly"></el-input>
            <!-- <el-input class="inputtwo" v-model="toaudit.material" readonly="readonly" v-else></el-input> -->
          </el-col>
          <el-col :span="2" :offset="1">
            <el-button type="primary" class="lookbtn" @click="copy()" v-if="this.toaudit.ifpic">复制</el-button>
            <el-button type="primary" class="lookbtn" @click="lookpic()" v-else>查看</el-button>
          </el-col>
        </el-row>
      </div>

      <div class="adspass">
        <el-row>
          <el-col :span="3" :offset="5">
            <el-button type="info" class="minbtn">审核结果</el-button>
          </el-col>
          <el-col :span="8">
            <el-col :span="11" :offset="1">
              <el-radio class="elrad" v-model="toaudit.rastate" label="7">通过</el-radio>
            </el-col>
            <el-col :span="12">
              <el-radio class="elrad2" v-model="toaudit.rastate" label="8">未通过</el-radio>
            </el-col>
          </el-col>
        </el-row>
        
      </div>

      <div class="adsnotpass">
        <el-row>
          <el-col :span="3" :offset="5" class="divminbtn">
            <el-button type="info" class="minbtn">未通过原因</el-button>
          </el-col>
          <el-col :span="9">
            <el-input
              type="textarea"
              :autosize="{ minRows: 5}"
              placeholder="请输入未通过的原因"
              v-model="toaudit.reason" clearable>
            </el-input>
          </el-col>
        </el-row>
      </div>

      <div class="adssure">
        <el-row>
          <el-col :span="4" :offset="3">
            <el-input type="hidden"></el-input>
          </el-col>
          <el-col :span="9">
            <el-col :span="6">
              <el-button type="primary" class="surebtn" @click="btnsure()">确认</el-button>
            </el-col>
            <el-col :span="12"></el-col>
            <el-col :span="6" :offset="12" class="rebackbtn">
              <el-button type="info" @click="btnreback()">取消</el-button>
            </el-col>
          </el-col>
        </el-row>
      </div>
    </div>
  </div>
</template>

<script>
    import '../css/adsmg.less'
    import '../js/axios.js'
    export default {
      name: "AdsMg",
      data () {
        return {
          index:{
            onclick1:true,      //通过点击改变按钮样式
            onclick2:false,
            onclick3:false,
            otherbg:false,         //其它证件遮罩v-if
            bgimg:true,            //遮罩图片三目运算符
            othersrc:'',           //其它证件src
            bgbtn:false,           //上一张下一张v-if
            predis:'',              //上一张按钮disable
            nextdis:'',            //下一张按钮disable
            bgif:false,            //遮罩v-if
            bgsrc1:'',            //遮罩图
            gbsrc2:'',
            picindex:0,            //其它证件索引
            tabledata:[],
            placeholder:'输入ID进行搜索',
            audit:true,           //审核v-if
            search:[              //搜索下拉列表
              {value:'ID',name:'ID搜索'},
              {value:'NAME',name:'广告名搜索'}
            ],
            select:'ID',           //搜索下拉值
            searchname:'',          //名字搜索
            currentPage:1,            //当前页
            pagesize:8,              //每页大小
            total:1,                    //总数
            pagin:true,                    //分页v-if
            auditing:true,         //审核按钮
          },
          toaudit:{
            cuinfo:'',           //客户主页
            audit:false,
            icpinput:'未上传',         //ICP证书
            businput:'未上传',         //营业执照
            idtyinput:'未上传',        //身份证件
            otherinput:'未上传',       //其它证件
            rastate:'7',           //审核单选
            raopen:'0',             //开户单选
            ificp:false,              //icp  v-if
            ifbus:false,              //营业执照v-if
            ifidty:false,             //身份证件 v-if
            ifother:false,            //其它证件 v-if
            ifpic:false,             //广告素材v-if
            characters:true,        //文字或图片v-if
            icpbtn:true,             //icp查看按钮
            material:'已上传',             //广告素材
            busbtn:true,             //营业执照查看按钮
            idtybtn:true,            //身份证件查看按钮
            otherbtn:true,           //其它证件查看按钮
            urlbtn:true,              //广告链接按钮
            reason:'',             //未通过的原因
            title:'',             //广告标题
            url:'',               //广告链接
            icpurl:'',
            bussomessurl:'',
            identityurl:[],
            otherurl:[],
            materialurl:'',
            playtype:'',             //投放类型
            selectitem1:'',           //广告品类1
            selectitem1s:[],
            selectitem2:'',           //广告品类2
            selectitem2s:[],
            adid:'',            //用于审核的id
          },
        }
      },
      methods:{
        nozzle(currentpage,pagesize,state,target,name){        //通过传参来进行请求
          var params={};
          var _this=this;
          _this.index.tabledata=[];
          if(name==''||target==''){
            params={
              pageNo:currentpage,
              pageSize:pagesize,
              state:state,
            };
          }else{
            params={
              pageNo:currentpage,
              pageSize:pagesize,
              state:state,
              tarVal:target,
              paramVal:name
            };
          }
          
          _this.$http.get('/Admin/AdsMg/GetAdRecordLists_vue',{
            params:params
          }).then(function(response){
            var info=response.data;
            if(info!=''||info!=null){
              _this.index.total=info.TotalCount;
              _this.index.tabledata=info.AdRecordInfo;
            }else{
              _this.index.total=0;
              _this.index.tabledata=[];
            }
            if(_this.index.total<=0){
              _this.index.pagin=false;
            }else{
              _this.index.pagin=true;
            }
          }).catch(function(error){
            console.log(error);
            _this.$message({
              message:'服务器错误',
              type:'error'
            });
          });
        },
        GetAdClassList() {                 //获取广告品类
          var _this=this;
            let classList = sessionStorage.getItem('typeData');
            if (classList === null || classList === '') {
              _this.$http.post('/CreateAd/GetAdClassList').then(function(response){
                sessionStorage.setItem('typeData', JSON.stringify(response.data));
              }).catch(function(error){
                console.log(error);
                _this.$message({
                  message:'服务器错误',
                  type:'error'
                });
              });
            }
            else {
                return;
            }
        },
        waitbtn(){    //待审核按钮
          this.index.onclick1=true;    //改变按钮样式
          this.index.onclick2=false;
          this.index.onclick3=false;
          this.index.auditing=true;
          var currentpage=this.index.currentPage;
          var pagesize=this.index.pagesize;
          var state='0';
          this.nozzle(currentpage,pagesize,state,'','');
        },
        passbtn(){    //已通过按钮
          this.index.onclick1=false;    //改变按钮样式
          this.index.onclick2=true;
          this.index.onclick3=false;
          this.index.auditing=false;
          var currentpage=this.index.currentPage;
          var pagesize=this.index.pagesize;
          var state='7';
          this.nozzle(currentpage,pagesize,state,'','');
        },
        notpass(){    //未通过按钮
          this.index.onclick1=false;    //改变按钮样式
          this.index.onclick2=false;
          this.index.onclick3=true;
          this.index.auditing=false;
          var currentpage=this.index.currentPage;
          var pagesize=this.index.pagesize;
          var state='8';
          this.nozzle(currentpage,pagesize,state,'','');
        },
        closebtn(){          //其它证件遮罩关闭按钮
          this.index.otherbg=false; 
        },
        bgclick(){         //点击遮罩层关闭
          this.index.bgif=false;
        },
        sername(){            //搜索按钮
          var _this=this;
          if(this.index.searchname==''){
            if(_this.index.select=='ID'){
              this.$message({
                message:'请输入要搜索的ID',
                type:'warning'
              });
              this.$refs.sername.focus();
              return;
            }else{
              this.$message({
                message:'请输入要搜索的广告名',
                type:'warning'
              });
              this.$refs.sername.focus();
              return;
            }
            
          }else{
            if(_this.index.onclick1==true){
              var currentpage=_this.index.currentPage;
              var pagesize=_this.index.pagesize;
              var state='0';
              var target=_this.index.select;
              var name=_this.index.searchname;
              _this.nozzle(currentpage,pagesize,state,target,name);
            }else if(_this.index.onclick2==true){
              var currentpage=_this.index.currentPage;
              var pagesize=_this.index.pagesize;
              var state='7';
              var target=_this.index.select;
              var name=_this.index.searchname;
              _this.nozzle(currentpage,pagesize,state,target,name);
            }else if(_this.index.onclick3==true){
              var currentpage=_this.index.currentPage;
              var pagesize=_this.index.pagesize;
              var state='8';
              var target=_this.index.select;
              var name=_this.index.searchname;
              _this.nozzle(currentpage,pagesize,state,target,name);
            }
            _this.index.searchname='';
          }
        },
        reset(){
          var _this=this;
          var state=0;
          if(_this.index.onclick1==true){
            state=0;
          }else if(_this.index.onclick2==true){
            state=7;
          }else if(_this.index.onclick3==true){
            state=8;
          }
          _this.index.searchname='';
          _this.nozzle(_this.index.currentPage,_this.index.pagesize,state,_this.index.select,'');
        },
        Auditing(row){           //审核
          var _this=this;
          _this.index.audit=false;
          _this.toaudit.audit=true;
          _this.toaudit.adid=row.AdId;
          _this.toaudit.selectitem1s=[];
          _this.toaudit.selectitem2s=[];
          _this.$http.post('/Admin/AdsMg/GetCheckInfo',{  //请求审核接口
            adId:row.AdId
          }).then(function(respnose){
            var info=respnose.data;
            if(info!=''||info!=null){
              _this.toaudit.cuinfo=info.HomePage;
              if(info.IcpUrl!=''||info.IcpUrl!=null){   //ICP证书
                _this.toaudit.icpinput='已上传';
                _this.toaudit.ificp=true;
                _this.toaudit.icpbtn=false;
                _this.toaudit.icpurl=info.IcpUrl;
              }else{
                _this.toaudit.icpinput='未上传';
                _this.toaudit.ificp=false;
                _this.toaudit.icpbtn=true;
                _this.toaudit.icpurl='';
              }

              if(info.BusinessLicence!=''||info.BusinessLicence!=null){    //营业执照
                _this.toaudit.businput='已上传';
                _this.toaudit.ifbus=true;
                _this.toaudit.busbtn=false;
                _this.toaudit.bussomessurl=info.BusinessLicence;
              }else{
                _this.toaudit.businput='未上传';
                _this.toaudit.ifbus=false;
                _this.toaudit.busbtn=true;
                _this.toaudit.bussomessurl='';
              }

              if(info.CardLicenceList!=''||info.CardLicenceList!=null){    //身份证件
                _this.toaudit.idtyinput='已上传';
                _this.toaudit.ifidty=true;
                _this.toaudit.idtybtn=false;
                if(info.CardLicenceList.length>1){
                  _this.toaudit.identityurl=info.CardLicenceList;
                }else{
                  _this.toaudit.identityurl.push(info.CardLicenceList);
                }
              }else{
                _this.toaudit.businput='未上传';
                _this.toaudit.ifidty=false;
                _this.toaudit.idtybtn=true;
                _this.toaudit.identityurl=[];
              }

              if(info.OtherLicenceList!=''||info.OtherLicenceList!=null){    //其它证件
                _this.toaudit.otherinput='已上传';
                _this.toaudit.ifother=true;
                _this.toaudit.otherbtn=false;
                if(info.OtherLicenceList.length>1){
                  _this.toaudit.otherurl=info.OtherLicenceList;
                }else{
                  _this.toaudit.otherurl.push(info.OtherLicenceList);
                }
              }else{
                _this.toaudit.otherinput='未上传';
                _this.toaudit.ifother=false;
                _this.toaudit.otherbtn=true;
                _this.toaudit.otherurl=[];
              }

              if(info.AdType==0){            //推广类型
                _this.toaudit.raopen=='0';
              }else if(info.AdType==1){
                _this.toaudit.raopen=='1';
              }else if(info.AdType==2){
                _this.toaudit.raopen=='2';
              }

              _this.toaudit.title=info.Title;
              if(info.AdUrl!=''||info.AdUrl!=null){      //广告链接
                _this.toaudit.urlbtn=false;
                _this.toaudit.url=info.AdUrl;
              }else{
                _this.toaudit.urlbtn=true;
                _this.toaudit.url='';
              }

              if(info.AdMaterialContents!=''||info.AdMaterialContents!=null){    //广告素材
                if(info.AdMaterialContents.match("http://(?!(\\.jpg|\\.png|\\.gif|\\.jpeg|\\.tiff|\\.bmp)).+?(\\.jpg|\\.png|\\.gif|\\.jpeg|\\.tiff|\\.bmp)")){
                  _this.toaudit.ifpic=false;
                  _this.toaudit.materialurl=info.AdMaterialContents;
                  _this.toaudit.material='已上传';
                }else{
                  _this.toaudit.ifpic=true;
                  _this.toaudit.materialurl='';
                  _this.toaudit.material=info.AdMaterialContents;
                }
              }else{
                _this.toaudit.ifpic=true;
                _this.toaudit.material='';
              }
              
              var playform=row.PayPlatform.replace('  ','')
              _this.toaudit.playtype=playform;
              //广告品类
              let Adlists = JSON.parse(sessionStorage.getItem('typeData'));
              var typeid='';
              _this.toaudit.selectitem1s=Adlists.classList;
              for (let index = 0; index < Adlists.jsonList.length; index++) {    //设置广告品类2
                if(row.AdCategoryId==Adlists.jsonList[index].Data.id){
                  typeid=Adlists.jsonList[index].Data.type_id;
                  _this.toaudit.selectitem2=Adlists.jsonList[index].Data.id;
                }
              }

              for (let index = 0; index < Adlists.jsonList.length; index++) {
                if(typeid==Adlists.jsonList[index].Data.type_id){
                  _this.toaudit.selectitem2s.push(Adlists.jsonList[index]);
                }
              }

              for (let index = 0; index < Adlists.classList.length; index++) {    //设置广告品类1
                if(typeid==Adlists.classList[index].Data.type_id){
                  _this.toaudit.selectitem1=Adlists.classList[index].Data.type_id;
                }
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
            message:'此功能暂未开发',
            type:'warning'
          });
        },
        selects(){           //下拉搜索改变
          var _this=this;
          if(_this.index.select=='ID'){
            _this.index.placeholder='输入ID进行搜索';
          }else{
            _this.index.placeholder='输入广告名进行搜索';
          }
        },
        lookicp(){           //icp查看
          this.index.bgif=true;
          this.index.bgsrc1=this.toaudit.icpurl;
          this.index.bgsrc2='';
          this.index.bgimg=true;
        },
        lookbus(){           //营业执照查看
          this.index.bgif=true;
          this.index.bgsrc1=this.toaudit.bussomessurl;
          this.index.bgsrc2='';
          this.index.bgimg=true;
        },
        lookidty(){           //身份证件查看
          this.index.bgif=true;
          this.index.bgimg=false;
          console.log();
          if(this.toaudit.identityurl.length>1){
            this.index.bgsrc1=this.toaudit.identityurl[0];
            this.index.bgsrc2=this.toaudit.identityurl[1];
          }else{
            this.index.bgsrc1=this.toaudit.identityurl[0];
            this.index.bgsrc2='';
          }
        },
        lookother(){         //其它证件查看
          this.index.picindex=0;
          this.index.otherbg=true;
          this.index.bgif=false;
          if(this.toaudit.otherurl.length<=1){
            this.index.bgbtn=false;
            this.index.predis=true;
            this.index.nextdis=true;
            this.index.othersrc=this.toaudit.otherurl[this.index.picindex];
          }else{
            this.index.bgbtn=true;
            this.index.predis=false;
            this.index.nextdis=false;
            this.index.othersrc=this.toaudit.otherurl[this.index.picindex];
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
            this.index.othersrc=this.toaudit.otherurl[this.index.picindex];
          }
        },
        nextdispic(){                //下一张
          this.index.predis=false;
          if(this.index.picindex>=(this.toaudit.otherurl.length-1)){
            this.$message({
              message:'这已经是最后一张了哦~',
              type:'warning'
            });
            this.index.nextdis=true;
          }else{
            this.index.picindex=this.index.picindex+1;
            this.index.othersrc=this.toaudit.otherurl[this.index.picindex];
          }
        },
        openurl(){              //打开广告链接
          var _this=this;
          if(_this.toaudit.url!=''||_this.toaudit.url!=null){
            window.open(_this.toaudit.url);
          }else{
            _this.$message({
              message:'暂无广告链接',
              type:'warning'
            });
          }
        },
        copy(){               //复制文字
          var _this=this;
          var url=document.querySelector('#material');
          url.select();
          if(document.execCommand('Copy')){
            document.execCommand('Copy');
            this.$message({
              message:'复制成功',
              type:'success'
            });
          }
        },
        lookpic(){             //查看广告素材
          this.index.bgif=true;
          this.index.bgsrc1=this.toaudit.materialurl;
          this.index.bgsrc2='';
          this.index.bgimg=true;
        },
        btnsure(){             //确认按钮
          var _this=this;
          if(_this.toaudit.rastate=='8'){
            if(_this.toaudit.reason==''){
              _this.$message({
                message:'请填写未通过的原因',
                type:'warning'
              });
              return;
            }
          }else{
            var info={
              AdId:_this.toaudit.adid,
              vType:_this.toaudit.rastate,
              vNote:_this.toaudit.reason
            };
            _this.$http.post('/Admin/AdsMg/CreateVerifyInfo',info).then(function(response){
              if(response.data.statusCode==1002){
                _this.$message({
                  message:'审核失败',
                  type:'error'
                });
              }else if(response.data.statusCode==1001){
                _this.$message({
                  message:'审核成功',
                  type:'success'
                });
                setTimeout(function(){
                  _this.toaudit.audit=false;
                  _this.index.audit=true;
                  var currentpage=_this.index.currentPage;
                  var pagesize=_this.index.pagesize;
                  var state='';
                  if(_this.index.onclick1==true){
                    state='0';
                    _this.nozzle(currentpage,pagesize,state,'','');
                  }else if(_this.index.onclick2==true){
                    state='7';
                    _this.nozzle(currentpage,pagesize,state,'','');
                  }else if(_this.index.onclick3==true){
                    state='8';
                    _this.nozzle(currentpage,pagesize,state,'','');
                  }
                },1500);
              }else if(response.data.statusCode==1003){
                _this.$message({
                  message:'审核备注不能为空',
                  type:'warning'
                });
              }else if(response.data.statusCode==1004){
                _this.$message({
                  message:'审核状态未改变',
                  type:'warning'
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
        btnreback(){            //取消按钮
          this.index.audit=true;
          this.toaudit.audit=false;
        },
        handleSizeChange(val) {    //分页功能
          var _this=this;
          this.index.pagesize=val;
          _this.index.currentPage=1;
          var currentpage=_this.index.currentPage;
          var pagesize=_this.index.pagesize;
          var state='';
          if(_this.index.onclick1==true){
            state='0';
            _this.nozzle(currentpage,pagesize,state,'','');
          }else if(_this.index.onclick2==true){
            state='7';
            _this.nozzle(currentpage,pagesize,state,'','');
          }else if(_this.index.onclick3==true){
            state='8';
            _this.nozzle(currentpage,pagesize,state,'','');
          }
          
        },
        handleCurrentChange(val) {    //当前页改变
          this.index.currentPage=val;
          var _this=this;
          var currentpage=_this.index.currentPage;
          var pagesize=_this.index.pagesize;
          var state='';
          if(_this.index.onclick1==true){
            state='0';
            _this.nozzle(currentpage,pagesize,state,'','');
          }else if(_this.index.onclick2==true){
            state='7';
            _this.nozzle(currentpage,pagesize,state,'','');
          }else if(_this.index.onclick3==true){
            state='8';
            _this.nozzle(currentpage,pagesize,state,'','');
          }
        },
      },
      created(){
        var _this=this;
        if(_this.toaudit.icpinput=='未上传'){
          _this.toaudit.icpbtn=true;
        }if(_this.toaudit.businput=='未上传'){
          _this.toaudit.busbtn=true;
        }if(_this.toaudit.idtyinput=='未上传'){
          _this.toaudit.idtybtn=true;
        }if(_this.toaudit.otherinput=='未上传'){
          _this.toaudit.otherbtn=true;
        }
        var currentpage=_this.index.currentPage;
        var pagesize=_this.index.pagesize;
        var state='0';
        _this.nozzle(currentpage,pagesize,state,'','');
        _this.GetAdClassList();
      }
    }
</script>

<style scoped>

</style>

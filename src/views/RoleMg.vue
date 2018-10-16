<template>
    <div class="roleindex">
      <div v-show="btnadd">       <!--主界面-->
        <el-row class="rowsearch">
          <el-col :span="2">
            <el-tag type="info" class="tag">名字：</el-tag>
          </el-col>
          <el-col :span="6" :offset="1">
            <el-input v-model="searchname" class="search" ref="search"
              placeholder="输入名字进行搜索" @keyup.enter.native="sername()" clearable>
            </el-input> 
          </el-col>

          <el-col :span="2" :offset="1">
            <el-button type="primary" @click="sername()">查询</el-button>
          </el-col>
          <el-col :span="2" :offset="1">
            <el-button type="primary" @click="reset()">重置</el-button>
          </el-col>
          <el-col :span="2" :offset="6" class="btnadd">
            <el-button type="primary" @click="addbtn()">添加</el-button>
          </el-col>
        </el-row>

        <el-row>
          <div class="divtable">       <!--表格部分-->
            <el-table stripe border 
                :data="tabledata"
                :default-sort = "{prop: 'StaffName', order: 'descending'}">
                <el-table-column
                  header-align="center"
                  align="center"
                  prop="StaffName"
                  label="名字"
                  >
                </el-table-column>
                <el-table-column
                  header-align="center"
                  align="center"
                  prop="LoginAccount"
                  sortable
                  label="登录账号">
                </el-table-column>
                <el-table-column
                  header-align="center"
                  align="center"
                  prop="Email"
                  label="邮箱">
                </el-table-column>
                <el-table-column
                  header-align="center"
                  align="center"
                  prop="RoleDesInfo"
                  label="角色">
                </el-table-column>
                <el-table-column
                  header-align="center"
                  align="center"
                  prop="StatusDesInfo"
                  label="状态">
                </el-table-column>
                <el-table-column
                  header-align="center"
                  align="center"
                  label="操作"
                  width="200px">
                  <template slot-scope="scope">
                    <el-button type="primary" @click="edit(scope.row)" size="small">编辑</el-button>
                    <el-tooltip class="item" effect="dark" content="此功能暂未开放！" placement="top">
                      <el-button type="warning" @click="freeze(scope.row)" size="small">冻结</el-button>
                    </el-tooltip>
                  </template>
                </el-table-column>
              </el-table>
            
          </div>
        </el-row>

        <el-row>
          <el-pagination class="pagin" v-show="pagin"
              @size-change="handleSizeChange"
              @current-change="handleCurrentChange"
              :current-page.sync="currentPage"
              :page-sizes="[2, 4, 6, 8, 10]"
              :page-size="pagesize"
              :pager-count="5"
              layout="total, sizes, prev, pager, next, jumper"
              :total="total">
          </el-pagination>
        </el-row>
      </div>

        <!--添加按钮界面-->
      <div v-show='roleadd' class="divadd"> 
        <el-row class="inputdiv">
          <el-col :span="3" :offset="6" class="elspans">
            <span class="spanname">员工名字：</span>
          </el-col>
          <el-col :span="7">
            <el-input placeholder="请输入员工名字" class="txtname" maxlength="50" v-model="addname" ref="focusname" clearable></el-input>
          </el-col>
        </el-row>

        <el-row class="inputdiv">
          <el-col :span="3" :offset="6" class="elspans">
            <span class="spanname">账&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;号：</span>
          </el-col>
          <el-col :span="7">
            <el-input placeholder="请输入账号" class="txtname" maxlength="50" v-model="addaccount" ref="focusacc" clearable></el-input>
          </el-col>
        </el-row>

        <el-row class="inputdiv">
          <el-col :span="3" :offset='6' class="elspans">
            <span class="spanname">密&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;码：</span>
          </el-col>
          <el-col :span="7">
            <el-input placeholder="请输入密码" maxlength="30" minlength="6" type="password" class="txtname" v-model="addpwd" ref="focuspwd" clearable></el-input>
          </el-col>
        </el-row>

        <el-row class="inputdiv">
          <el-col :span="3" :offset='6' class="elspans">
            <span class="spanname">邮&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;箱：</span>
          </el-col>
          <el-col :span="7">
            <el-input placeholder="请输入邮箱" class="txtname" maxlength="50" v-model="addemail" ref="focusemail" clearable></el-input>
          </el-col>
        </el-row>

        <div class="inputdiv">
          <el-row>
            <el-col :span="3" :offset='6' class="power elspans">
              <span class="spanname">权限职位：</span>
            </el-col>
            <el-col :span="7">
              <el-radio-group v-model="radio" class="divrad">
                <el-row>
                  <el-col :span="8">
                    <el-radio label="1" :click="rabtn()">运营</el-radio>
                  </el-col>
                  <el-col :span="8">
                    <el-radio label="2" :click="rabtn()">运营管理员</el-radio>
                  </el-col>
                  <el-col :span="8">
                    <el-radio label="3" :click="rabtn()">代理商</el-radio>
                  </el-col>
                </el-row>

                <el-row>
                  <el-col :span="8">
                    <el-select v-show="raoper" v-model="operation" class="select" placeholder="隶属管理员">
                      <el-option v-for="item in operlist" :key="item.AccountId" :label="item.StaffName" :value="item.AccountId">
                        
                      </el-option>
                    </el-select>
                  </el-col>
                  <el-col :span="8" :offset="8">
                    <el-select v-show="raoperad" v-model="operadmin" class="select" placeholder="隶属代理商">
                      <el-option v-for="item in dlist" :key="item.AccountId" :label="item.StaffName" :value="item.AccountId">
                        
                      </el-option>
                    </el-select>
                  </el-col>
                  <el-col :span="8" :offset="16">
                    <input v-show="ragent" placeholder="(0~100)" class="rebate" maxlength="3" v-model="agent" ref="agent" clearable />
                  </el-col>
                </el-row>

                <el-row>
                  <el-col :span="8">
                    <el-radio label="4" :click="rabtn()">销售</el-radio>
                  </el-col>
                  <el-col :span="8">
                    <el-radio label="5" :click="rabtn()">广告审核</el-radio>
                  </el-col>
                  <el-col :span="8">
                    <el-radio label="6" :click="rabtn()">财务制表</el-radio>
                  </el-col>
                </el-row>
                
                <el-row>
                  <el-col :span="8">
                    <el-radio label="7" :click="rabtn()">财务授权</el-radio>
                  </el-col>
                  <el-col :span="8">
                    <el-radio label="8" :click="rabtn()">客户审核</el-radio>
                  </el-col>
                  <el-col :span="8">
                    <el-radio label="9" :click="rabtn()">流量方审核</el-radio>
                  </el-col>
                </el-row>
              </el-radio-group>
            </el-col>
          </el-row>
          
        </div>

        <el-row>
          <el-col :span="2" :offset="8" class="footerbtn">
            <el-button type="info" plain @click="reback">返回</el-button>
          </el-col>
          <el-col :span="2" :offset="4" class="footerbtn">
            <el-button type="primary" @click="btnsure()">确认添加</el-button>
          </el-col>
        </el-row>
      </div>

      <!--编辑层-->
    <div v-show="refresh" class="refresh">
        <el-row class="inputdiv">
          <el-col :span="3" :offset="6" class="elspans">
            <span class="spanname">员工名字：</span>
          </el-col>
          <el-col :span="7">
            <el-input placeholder="请输入员工名字" class="txtnames" maxlength="50" v-model="refname" ref="frsname" clearable readonly></el-input>
          </el-col>
        </el-row>
        <el-row class="inputdiv">
          <el-col :span="3" :offset='6' class="elspans">
            <span class="spanname">邮&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;箱：</span>
          </el-col>
          <el-col :span="7">
            <el-input placeholder="请输入邮箱" class="txtname" v-model="refemail" maxlength="50" minlength="6" ref="frsemail" clearable></el-input>
          </el-col>
        </el-row>
        <el-row class="inputdiv">
          <el-col :span="3" :offset='6' class="elspans">
            <span class="spanname">密&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;码：</span>
          </el-col>
          <el-col :span="7">
            <el-input placeholder="请输入密码" type="password" maxlength="30" minlength="6" class="txtname" v-model="refpwd" ref="frspwd" clearable></el-input>
          </el-col>
        </el-row>

        <el-row class="inputdiv" v-show="refbate">
          <el-col :span="3" :offset='6' class="elspans">
            <span class="spanname">返点比例：</span>
          </el-col>
          <el-col :span="7">
            <el-input placeholder="请输入返点比例" type="text" maxlength="3" class="txtname" v-model="refbates" ref="rebates" clearable></el-input>
          </el-col>
        </el-row>

        <el-row class="rowfooterbtn">
          <el-col :span="2" :offset="8" class="footerbtn">
            <el-button type="info" plain @click="reback">返回</el-button>
          </el-col>
          <el-col :span="2" :offset="3" class="footerbtn">
            <el-button type="primary" @click="btnfresh()">确认更新</el-button>
          </el-col>
        </el-row>
      </div>
    </div>
</template>
 
<script>
  import '../css/rolemg.less'
  import '../css/comview.less'
  
  import md5 from 'js-md5'
  import '../js/axios.js'
  export default {
    name: 'RoleMg',
    data () {
      return {
        btnadd:true,           //添加按钮v-if
        raoper:false,          //单选按钮运营v-if
        raoperad:false,          //单选按钮运营管理v-if
        ragent:false,          //输入框返点v-if
        refresh:false,          //更新按钮v-if
        roleadd:false,           
        searchname:'',         //搜索名字框
        addaccount:'',         //添加员工账号
        addpwd:'',             //添加密码
        addemail:'',           //添加邮箱
        addname:'',            //添加员工名字
        refname:'',            //更新员工名字
        refemail:'',           //更新员工邮箱
        refpwd:'',             //更新员工密码
        refid:'',
        refbate:false,
        refbates:'',
        radio:"1",             //单选按钮
        operation:'',          //隶属运营管理员
        operlist:[],           //运营管理员列表
        dlist:[],              //代理商列表
        tabledata:[],
        operadmin:'',          //隶属代理商
        agent:'',               //代理商返点比例
        currentPage:1,         //分页
        pagesize:8,            //每页数量
        total:0,              //数据总数
        pagin:true,
      }
    },
    methods:{
      nozzle(currentpage,pagesize,staffname){     //通过传参来查询数据
        var _this=this;
        _this.tabledata=[];
        var params={};
        if(staffname==''||staffname==null){
          params={
            pageNo:currentpage,
            pageSize:pagesize
          };
        }else{
          params={
            pageNo:currentpage,
            pageSize:pagesize,
            staffName:staffname
          };
        }

        _this.$http.get('/Admin/RoleMg/GetRoleLists_Vue',{
          params:params
        }).then(function(response){
          var info=response.data;
          if(info!=''||info!=null){
            _this.total=info.TotalCount;
            _this.tabledata=info.RolesInfoDTOS;
          }else{
            _this.total=0;
            _this.tabledata=[];
          }

          if(_this.total<=0){
            _this.pagin=false;
          }else{
            _this.pagin=true;
          }
        }).catch(function(error){
          _this.$message({
            message:'服务器错误',
            type:'warning'
          });
        });
      },
      reset(){                      //重置
        var _this=this;
        _this.searchname='';
        _this.nozzle(_this.currentPage,_this.pagesize,'');
      },
      addbtn(){       //添加按钮
        this.btnadd=false;
        this.refresh=false;
        this.roleadd=true;
        this.addaccount='';     //清空输入框,v-if不刷新页面
        this.addpwd='';
        this.addemail='';
        this.addname='';
        this.radio='1';
        this.operation='';
        this.operadmin='';
      },
      reback(){       //返回按钮
        this.btnadd=true;
        this.refresh=false;
        this.roleadd=false;
        this.searchname='';
        this.refpwd='';
        this.refbates='';
      },
      rabtn(){     //单选事件
        if(this.radio=="1"){
          this.raoper=true;
          this.raoperad=false;
          this.ragent=false;
        }else if(this.radio=="2"){
          this.raoper=false;
          this.raoperad=true;
          this.ragent=false;
        }else if(this.radio=="3"){
          this.raoper=false;
          this.raoperad=false;
          this.ragent=true;
        }else{
          this.raoper=false;
          this.raoperad=false;
          this.ragent=false;
        }
      },
      sername(){     //搜索名字按钮
        var _this=this;
        if(this.searchname==''){
          this.$message({
            message:'输入名字进行搜索',
            type:'warning'
          });
          _this.$refs.search.focus();
          return;
        }else{
          _this.nozzle(_this.currentPage,_this.pagesize,_this.searchname);
        }
      },
      btnsure(){     //添加员工方法
        var _this=this;    //全局变量
        var regEmail= /^([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$/;
        var regnum = /^\+?[0-9][0-9]*$/;    //正则校验
        if(_this.addname==''){
          _this.$message({
						message: '请输入员工姓名',
						type: 'warning'
          });
          this.$refs.focusname.focus();
          return;
        }else if(_this.addaccount==''){
          this.$message({
						message: '请输入账号',
						type: 'warning'
          });
          this.$refs.focusacc.focus();
          return;
        }else if(_this.addpwd==""){
          this.$message({
						message: '请输入密码',
						type: 'warning'
          });
          this.$refs.focuspwd.focus();
          return;
        }else if(parseInt(_this.addpwd.length)<6){
          this.$message({
						message: '密码不能少于六位',
						type: 'warning'
          });
          this.$refs.focuspwd.focus();
          return;
        }else if(_this.addemail==''){
          this.$message({
						message: '邮箱不能为空',
						type: 'warning'
          });
          this.$refs.focusemail.focus();
          return;
        }else if(!regEmail.test(_this.addemail)){
          this.$message({
						message: '邮箱格式不正确',
						type: 'warning'
          });
          this.$refs.focusemail.focus();
          return;
        }else{
          var valueid='';
          if(_this.radio=='1'){
            if(_this.operation==''){
              this.$message({
                message: '请选择运营隶属管理员',
                type: 'warning'
              });
              return;
            }
            valueid=_this.operation;
          }else if(_this.radio=='2'){
            valueid=_this.operadmin;
          }else if(_this.radio=='3'){
              if(_this.agent==''){
                this.$message({
                  message: '请输入代理商返点比例',
                  type: 'warning'
                });
                this.$refs.agent.focus();
                return;
              }else if(!regnum.test(_this.agent)){
                this.$message({
                  message: '请输入正确的返点比例',
                  type: 'warning'
                });
                this.$refs.agent.focus();
                return;
              }else if(parseInt(_this.agent)>100||parseInt(_this.agent)<0){
                this.$message({
                  message: '返点比例必须在0-100',
                  type: 'warning'
                });
                this.$refs.agent.focus();
                return;
              }
              valueid=_this.agent;
           }
          this.$http.post('/admin/rolemg/addRole',{
            staffname:_this.addname,
            account:_this.addaccount,
            password:md5(_this.addpwd),
            Email:_this.addemail,
            roleType:_this.radio,
            valueid:valueid
          }).then(function(response){
            if(response.data.status=='ok'){
              _this.$message({
                message:response.data.info,
                type:'success'
              });
              setTimeout(function(){
                _this.btnadd=true;
                _this.roleadd=false;
              },1000);
            }else if(response.data.status=='no'){
              _this.$message({
                message:response.data.info,
                type:'error'
              });
            }else{
              _this.$message({
              message:response.data.info,
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
      btnfresh(){   //更新员工
        var _this=this;
        var regEmail= /^([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$/;
        var regnum = /^\+?[0-9][0-9]*$/;    //正则校验
        if(_this.refname==''){
           this.$message({
              message: '请输入员工姓名',
              type: 'warning'
            });
            this.$refs.frsname.focus();
            return;
        }else if(_this.refemail==''){
          this.$message({
              message: '请输入邮箱',
              type: 'warning'
            });
            this.$refs.frsemail.focus();
            return;
        }else if(_this.refpwd==''){
            this.$message({
              message: '请输入密码',
              type: 'warning'
            });
            this.$refs.frspwd.focus();
            return;
        }else if(!regEmail.test(_this.refemail)){
          this.$message({
						message: '邮箱格式不正确',
						type: 'warning'
          });
          this.$refs.frsemail.focus();
          return;
        }else if(parseInt(_this.refpwd).length<6){
          this.$message({
              message: '密码不能少于六位',
              type: 'warning'
            });
            this.$refs.frspwd.focus();
            return;
        }else{
          var info={};
          if(_this.refbates==''){
            info={
              AccountId:_this.refid,
              password:md5(_this.refpwd),
              Email:_this.refemail,
              Rebate:''
            };
          }else{
            if(!regnum.test(_this.refbates)){
              this.$message({
                  message: '请输入正确的返点比例',
                  type: 'warning'
                });
                this.$refs.rebates.focus();
                return;
            }else if(parseInt(_this.refbates)<0||parseInt(_this.refbates)>100){
              this.$message({
                  message: '返点比例必须在0~100之间',
                  type: 'warning'
                });
              this.$refs.refbate.focus();
              return;
            }
            info={
              AccountId:_this.refid,
              password:md5(_this.refpwd),
              Email:_this.refemail,
              Rebate:_this.refbates
            };
          }
          _this.$http.post('/Admin/Rolemg/EditRole',info).then(function(response){
            if(response.data.status=='no'){
              _this.$message({
                message:response.data.info,
                type:'warning'
              });
            }else{
              _this.$message({
                message:'更新成功',
                type:'success'
              });
              setTimeout(function(){
                _this.refresh=false;
                _this.btnadd=true;
                _this.nozzle(_this.currentPage,_this.pagesize,'');
              },1500);
              _this.refpwd='';
              _this.refbates='';
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
      edit(row){                //编辑
        var _this=this;
        _this.btnadd=false;
        _this.refresh=true;
        _this.refid=row.AccountId;
        _this.refname=row.StaffName;
        _this.refemail=row.Email;
        if(row.RoleDesInfo=='代理商'){
          _this.refbate=true;
        }else{
          _this.refbate=false;
        }
      },
      freeze(){            //冻结
        var _this=this;
        _this.$message({
          message:'此功能暂未开发，敬请期待',
          type:'warning'
        });
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
      },
      handleSizeChange(val) {    //分页功能
        this.pagesize=val;
        this.currentPage=1;
        var currentpage=this.currentPage;
        var pagesize=this.pagesize;
        this.nozzle(currentpage,pagesize,'');
      },
      handleCurrentChange(val) {    //当前页改变
        this.currentPage=val;
        var currentpage=this.currentPage;
        var pagesize=this.pagesize;
        this.nozzle(currentpage,pagesize,'');
      }
    },
    mounted() {
      
    },
    created(){
      var _this=this;
       this.$http.post('/Admin/RoleMg/GetOpsAgecysList',{   //运营隶属管理员列表
          AccountRole:2
        }).then(function(response){
          _this.operlist=response.data.info;
        }).catch(function(error){
          console.log(error);
          _this.$message({
            message:'服务器错误',
            type:'error'
          });
        });

        this.$http.post('/Admin/RoleMg/GetOpsAgecysList',{  //运营管理员隶属代理商列表
          AccountRole:3
        }).then(function(response){
          _this.dlist=response.data.info;
        }).catch(function(error){
          console.log(error);
          _this.$message({
            message:'服务器错误',
            type:'error'
          });
        });

        _this.nozzle(_this.currentPage,_this.pagesize,'');
     }
  }
</script>
<style scoped>
 
</style>
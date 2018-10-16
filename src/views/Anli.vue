<template>
  <div class="anli">
    <!--查看图片遮罩-->
    <transition  name="fade">
      <div class="wrap_img" v-show="index.bgif" @click="bgclick">
        <img :src="index.bgsrc" class="bigimg2">
      </div>
    </transition>

    <div v-show="index.index">
      <el-row class="rowtitle">
        <el-col :span="2">
          <el-tag type="info" class="tag">标题</el-tag>
        </el-col>
        <el-col :span="6" :offset="1">
            <el-input v-model="index.title" class="search" ref="sername"
              placeholder="输入标题搜索" @keyup.enter.native="sername()" clearable>
            </el-input> 
        </el-col>
        <el-col :span="2" :offset="1">
          <el-button type="primary" @click="sername()">查询</el-button>
        </el-col>
        <el-col :span="2" :offset="1">
          <el-button type="primary" @click="reset()">重置</el-button>
        </el-col>
        <el-col :span="2" class="btnadd">
          <el-button type="primary" @click="addbtn()">添加</el-button>
        </el-col>
      </el-row>

      <el-row class="rowtable">
        <el-table border 
          :data="index.tabledata"
          :default-sort = "{prop: 'title', order: 'descending'}">
          <el-table-column
            header-align="center"
            align="center"
            prop="title"
            sortable
            label="标题">
          </el-table-column>
          <el-table-column 
            header-align="center"
            align="center"
            prop="industry"
            label="行业">
          </el-table-column>
          <el-table-column
            header-align="center"
            align="center"
            prop="data"
            label="数据">
          </el-table-column>
          <el-table-column 
            header-align="center"
            align="center"
            prop="jurl"
            label="链接">
          </el-table-column>
          <el-table-column
            header-align="center"
            align="center"
            label="图片">
            <template slot-scope="scope">
              <el-button type="primary" @click="looking(scope.row)" size="small">查看</el-button>
            </template>
          </el-table-column>
          <el-table-column 
            header-align="center"
            align="center"
            label="操作">
            <template slot-scope="scope">
              <el-button type="primary" @click="edit(scope.row)" size="small">编辑</el-button>
            </template>
          </el-table-column>
        </el-table>
      </el-row>

      <el-row class="pagin" v-show="index.pagin">
        <el-pagination class="pagin"
            @size-change="handleSizeChange"
            @current-change="handleCurrentChange"
            :current-page.sync="index.currentPage"
            :page-sizes="[2, 4, 6,8,10]"
            :page-size="index.pagesize"
            :pager-count="5"
            layout="total, sizes, prev, pager, next, jumper"
            :total="index.total">
        </el-pagination>
      </el-row>
    </div>

    <!--添加案例-->
    <div v-show="anliadd.index" class="anliadd">
      <el-row class="addrowtitle">
        <el-col :span="2" class="elspan" :offset="7">
          <span>标题：</span>
        </el-col>
        <el-col :span="6">
            <el-input v-model="anliadd.title" class="search" ref="sernames"
              placeholder="请输入标题" clearable>
            </el-input> 
        </el-col>
      </el-row>

      <el-row class="addrowtitle">
        <el-col :span="2" class="elspan" :offset="7">
          <span>链接：</span>
        </el-col>
        <el-col :span="6">
            <el-input v-model="anliadd.url" class="search" ref="serurl"
              placeholder="请输入链接" clearable>
            </el-input> 
        </el-col>
      </el-row>

      <el-row class="addrowtitle">
        <el-col :span="2" class="elspan" :offset="7">
          <span>数据：</span>
        </el-col>
        <el-col :span="6">
            <el-input v-model="anliadd.data" class="search" ref="serdata"
              placeholder="请输入数据" clearable>
            </el-input> 
        </el-col>
      </el-row>

      <el-row class="addrowtitle">
        <el-col :span="2" class="elspan" :offset="7">
          <span>行业：</span>
        </el-col>
        <el-col :span="6">
            <el-input v-model="anliadd.industry" class="search" ref="serindustry"
              placeholder="请输入行业" clearable>
            </el-input> 
        </el-col>
      </el-row>

      <el-row class="addrowtitle">
        <el-col :span="2" class="elspan" :offset="7">
          <span>图片：</span>
        </el-col>
        <el-col :span="5" class="elpic">
            <img src="../../src/assets/images/loginbg.png" v-if="anliadd.picshow"/>
            <img :src="anliadd.picpath" @click="clickpictr()" v-else />
        </el-col>
        <el-col :span="1">
          <el-upload
            class="upload-demo"
            :action="anliadd.actionurl"
            :on-success="addsuccess"
            name="imgFile"
            :show-file-list="false"
            list-type="picture">
            <el-button type="primary">上传</el-button>
          </el-upload>
        </el-col>
      </el-row>

      <el-row class="addrowbtn">
        <el-col :span="2" :offset="8">
          <el-button type="info" @click="addreback()">返回</el-button>
        </el-col>

        <el-col :span="2" :offset="3">
          <el-button type="primary" @click="addsure()">确认</el-button>
        </el-col>
      </el-row>
    </div>

    <!--编辑案例-->
    <div v-show="anlieditor.index" class="anliadd">
      <el-row class="addrowtitle">
        <el-col :span="2" class="elspan" :offset="7">
          <span>标题：</span>
        </el-col>
        <el-col :span="6">
            <el-input v-model="anlieditor.title" class="search" ref="edinames"
              placeholder="请输入标题" clearable>
            </el-input> 
        </el-col>
      </el-row>

      <el-row class="addrowtitle">
        <el-col :span="2" class="elspan" :offset="7">
          <span>链接：</span>
        </el-col>
        <el-col :span="6">
            <el-input v-model="anlieditor.url" class="search" ref="ediurl"
              placeholder="请输入链接" clearable>
            </el-input> 
        </el-col>
      </el-row>

      <el-row class="addrowtitle">
        <el-col :span="2" class="elspan" :offset="7">
          <span>数据：</span>
        </el-col>
        <el-col :span="6">
            <el-input v-model="anlieditor.data" class="search" ref="edidata"
              placeholder="请输入数据" clearable>
            </el-input> 
        </el-col>
      </el-row>

      <el-row class="addrowtitle">
        <el-col :span="2" class="elspan" :offset="7">
          <span>行业：</span>
        </el-col>
        <el-col :span="6">
            <el-input v-model="anlieditor.industry" class="search" ref="edindustry"
              placeholder="请输入行业" clearable>
            </el-input> 
        </el-col>
      </el-row>

      <el-row class="addrowtitle">
        <el-col :span="2" class="elspan" :offset="7">
          <span>图片：</span>
        </el-col>
        <el-col :span="3" class="elpic">
            <img src="../../src/assets/images/loginbg.png" v-if="anlieditor.picshow"/>
            <img :src="anlieditor.picpath" @click="clickpictr()" v-else />
        </el-col>
        <el-col :span="1">
          <el-upload
            class="upload-demo"
            action="/ToolFiles/kindeditor/asp.net/imguploadToOss.ashx?modulename=aptitude&uid=77ab93"
            :on-success="edisuccess"
            name="imgFile"
            :show-file-list="false"
            list-type="picture">
            <el-button type="primary">上传</el-button>
          </el-upload>
        </el-col>
        <el-col :span="1" :offset="1">
          <el-button type="primary" @click="edilook()" :disabled='anlieditor.lookbtn'>查看</el-button>
        </el-col>
      </el-row>

      <el-row class="addrowbtn">
        <el-col :span="2" :offset="8">
          <el-button type="info" @click="addreback()">返回</el-button>
        </el-col>

        <el-col :span="2" :offset="3">
          <el-button type="primary" @click="edisure()">确认</el-button>
        </el-col>
      </el-row>
    </div>
  </div>
</template>

<script>
  import '../css/anli.less'
  import '../js/axios.js'

  export default {
    name: "Anli",
    data () {
      return {
        index:{                   //首页
          index:true,
          title:'',
          tabledata:[],
          tabledatas:[],
          bgsrc:'',
          bgif:false,
          currentPage:1,            //当前页
          pagesize:8,              //每页大小
          total:1,                    //总数
          pagin:true,
        },
        anliadd:{                 //添加案例
          index:false,
          actionurl:'',
          title:'',
          url:'',
          data:'',
          industry:'',
          picpath:'',
          picture:'',
          picshow:true,                 //上传图片
        },
        anlieditor:{                 //编辑案例
          index:false,
          id:'',
          title:'',
          url:'',
          data:'',
          industry:'',
          picpath:'',
          picture:'',
          picshow:true,                 //上传图片
          lookbtn:false,                //查看图片disable
        },
      }
    },
    methods:{
      nozzle(pagecurrent,pagesize,name){
        var _this=this;
        _this.index.tabledata=[];
        var params={};
        if(name==''){
          params={
            pageNo:pagecurrent,
            pageSize:pagesize
          };
        }else{
          params={
            pageNo:pagecurrent,
            pageSize:pagesize,
            name:name
          };
        }
        _this.$http.get('/Admin/ANLI/GetAnLists_vue',{
          params:params
        }).then(function(response){
          var info=response.data;
          if(info!=''||info!=null){
            _this.index.total=info.TotalCount;
            _this.index.tabledata=info.AnLiInfo;
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
      GetIndex(){                      //获取ID
        var _this=this;
        _this.$http.post('/Admin/ANLI/VueGetIndex').then(function(response){
          var info=response.data;
          if(info!=''||info!=null){
            _this.anliadd.actionurl='/ToolFiles/kindeditor/asp.net/imguploadToOss.ashx?modulename=aptitude&uid='+info;
          }else{
            _this.index.actionurl='';
          }
        }).catch(function(error){
          console.log(error);
          _this.$message({
            message:'服务器错误',
            type:'error'
          });
        });
      },
      sername(){          //搜索
        if(this.index.title==''){
          this.$message({
            message:'请输入要搜索的标题内容',
            type:'warning'
          });
          this.$refs.sername.focus();
          return;
        }else{
          var _this=this;
          var currentpage=_this.index.currentPage;
          var pagesize=_this.index.pagesize;
          var name=_this.index.title;
          _this.nozzle(currentpage,pagesize,name);
          _this.index.title='';
        }
      },
      reset(){                       //重置
        var _this=this;
        _this.index.title='';
        var currentpage=_this.index.currentPage;
        var pagesize=_this.index.pagesize;
        _this.nozzle(currentpage,pagesize,'');
      },
      addbtn(){               //添加案例
        this.index.index=false;
        this.anliadd.index=true;
        this.anliadd.title='';
        this.anliadd.url='';
        this.anliadd.data='';
        this.anliadd.industry='';
        this.anliadd.picpath='';
        this.anliadd.picshow=true;
      },
      addreback(){             //添加案例返回
        this.anliadd.index=false;
        this.index.index=true;
        this.anlieditor.index=false;
      },
      addsure(){              //添加案例
        var match = /^((ht|f)tps?):\/\/[\w\-]+(\.[\w\-]+)+([\w\-\.,@?^=%&:\/~\+#]*[\w\-\@?^=%&\/~\+#])?$/;
        if(this.anliadd.title==''){
          this.$message({
            message:'请输入标题',
            type:'warning'
          });
          this.$refs.sernames.focus();
          return;
        }else if(this.anliadd.url==''){
          this.$message({
            message:'请输入链接',
            type:'warning'
          });
          this.$refs.serurl.focus();
          return;
        }else if(this.anliadd.data==''){
          this.$message({
            message:'请输入数据',
            type:'warning'
          });
          this.$refs.serdata.focus();
          return;
        }else if(this.anliadd.industry==''){
          this.$message({
            message:'请输入行业',
            type:'warning'
          });
          this.$refs.serindustry.focus();
          return;
        }else if(this.anliadd.picpath==''){
          this.$message({
            message:'请上传图片',
            type:'warning'
          });
          return;
        }else if(match.test(this.anliadd.url)){
          this.$message({
            message:'请输入正确的链接',
            type:'warning'
          });
          return;
        }else{
          var _this=this;
          var picimg='/'+_this.anliadd.picture;
          var addinfo={
            titlename:_this.anliadd.title,
            jurl:_this.anliadd.url,
            jdata:_this.anliadd.data,
            jimg:picimg,
            industry:_this.anliadd.industry
          }
          _this.$http.post('/Admin/ANLi/AddAnli',addinfo).then(function(response){
            if(response.data.status=='no'){
              _this.$message({
                message:'添加失败',
                type:'error'
              });
            }else{
              _this.$message({
                message:'添加成功，即将跳往首页',
                type:'success'
              });
              setTimeout(function(){
                _this.anliadd.index=false;
                _this.index.index=true;
                _this.nozzle(_this.index.currentPage,_this.index.pagesize,'');
              },1000);
            }
          }).catch(function(error){
            _this.$message({
              message:'服务器错误',
              type:'error'
            });
          });
        }
      },
      clickpictr(){          //点击查看图片
        this.index.bgif=true;
      },
      bgclick(){                //遮罩点击
        this.index.bgif=false;
      },
      edilook(){                   //编辑查看图片
        this.index.bgif=true;
        this.anlieditor.picpath=this.index.bgsrc;
      },
      editorbtn(){               //编辑查看图片
        if(this.anlieditor.picpath==''){
          this.anlieditor.lookbtn=true;
        }else{
          this.anlieditor.lookbtn=false;
        }
      },
      looking(row){              //表格查看图片
        this.index.bgif=true;
        this.index.bgsrc=row.imgurl;
      },
      edit(row){                    //编辑按钮
        this.anlieditor.id=row.id;
        this.anlieditor.title=row.title;
        this.anlieditor.url=row.jurl;
        this.anlieditor.data=row.data;
        this.anlieditor.industry=row.industry;
        this.index.index=false;
        this.anlieditor.index=true;
        var _this=this;
        console.log();
        if(row.imgurl!=''){
          _this.index.bgsrc=row.imgurl;
          _this.anlieditor.lookbtn=false;
          _this.anlieditor.picshow=false;
          _this.anlieditor.picpath=row.imgurl;
        }else{
          _this.index.bgsrc='';
          _this.anlieditor.lookbtn=true;
          _this.anlieditor.picshow=true;
          _this.anlieditor.picpath='';
        }
      },
      edisure(){                //编辑确认
        var match = /^((ht|f)tps?):\/\/[\w\-]+(\.[\w\-]+)+([\w\-\.,@?^=%&:\/~\+#]*[\w\-\@?^=%&\/~\+#])?$/;
        if(this.anlieditor.title==''){
          this.$message({
            message:'请输入标题',
            type:'warning'
          });
          this.$refs.edinames.focus();
          return;
        }else if(this.anlieditor.url==''){
          this.$message({
            message:'请输入链接',
            type:'warning'
          });
          this.$refs.ediurl.focus();
          return;
        }else if(this.anlieditor.data==''){
          this.$message({
            message:'请输入数据',
            type:'warning'
          });
          this.$refs.edidata.focus();
          return;
        }else if(this.anlieditor.industry==''){
          this.$message({
            message:'请输入行业',
            type:'warning'
          });
          this.$refs.edindustry.focus();
          return;
        }else if(this.anlieditor.picpath==''){
          this.$message({
            message:'请上传图片',
            type:'warning'
          });
          return;
        }else if(match.test(this.anliadd.url)){
          this.$message({
            message:'请输入正确的链接',
            type:'warning'
          });
          this.$refs.ediurl.focus();
          return;
        }else{
          var _this=this;
          var picimg='/'+_this.anlieditor.picture;
            var info={
              id:_this.anlieditor.id,
              titlename:_this.anlieditor.title,
              jurl:_this.anlieditor.url,
              jdata:_this.anlieditor.data,
              jimg:picimg,
              industry:_this.anlieditor.industry
            };
          _this.$http.post('/Admin/ANLI/EditAnLi',info).then(function(response){
            if(response.data.status=='no'){
              _this.$message({
                message:'更新失败',
                type:'error'
              });
            }else{
              _this.$message({
                message:'更新成功',
                type:'success'
              });
              setTimeout(function(){
                _this.anlieditor.index=false;
                _this.index.index=true;
                _this.nozzle(_this.index.currentPage,_this.index.pagesize,'');
              },1500);
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
      addsuccess(response,file,fileList){    //添加上传成功的钩子
        if(response.error!=0){
          this.$message({
            message:response.message,
            type:'warning'
          });
          this.anliadd.picshow=true;
          this.anliadd.picpath='';
          this.index.bgsrc='';
          this.anliadd.picture='';
        }else{
          this.$message({
            message:'上传成功',
            type:'success'
          });
          this.anliadd.picshow=false;
          this.anliadd.picpath=file.url;
          this.index.bgsrc=file.url;
          this.anliadd.picture=response.url;
        }
        
      },
      edisuccess(response,file,fileList){                           //编辑上传成功的钩子
        if(response.error!=0){
          this.$message({
            message:response.message,
            type:'warning'
          });
          this.anlieditor.picshow=true;
          this.anlieditor.picpath='';
          this.index.bgsrc='';
          this.anlieditor.picture='';
        }else{
          this.$message({
            message:'上传成功',
            type:'success'
          });
          this.anlieditor.picshow=false;
          this.anlieditor.picpath=file.url;
          this.index.bgsrc=file.url;
          this.anlieditor.picture=response.url;
        }
      },
      handleSizeChange(val) {    //分页功能
        this.index.pagesize=val;
        var _this=this;
        _this.index.currentPage=1;
        var currentpage=_this.index.currentPage;
        var pagesize=_this.index.pagesize;
        _this.nozzle(currentpage,pagesize,'');
      },
      handleCurrentChange(val) {    //当前页改变
        var _this=this;
        _this.index.currentPage=val;
        var currentpage=_this.index.currentPage;
        var pagesize=_this.index.pagesize;
        _this.nozzle(currentpage,pagesize,'');
        
      },
    },
    created(){
      var _this=this;
      _this.GetIndex();
      var currentpage=_this.index.currentPage;
      var pagesize=_this.index.pagesize;
      _this.nozzle(currentpage,pagesize,'');
    },
  }
</script>

<style scoped>

</style>

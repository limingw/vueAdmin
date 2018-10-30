<template>
  <div class="nodemonitor">
    <div class="index">
      <el-row class="sercondition">
        <el-col :span="3" class="elh2">
          <h2>节点监控</h2>
        </el-col>

        <el-col :span="2" :offset="0" class="elspan martop">
          <span>支付平台：</span>
        </el-col>
        <el-col :span="3" class="martop">
          <el-select v-model="terrace" placeholder="请选择">
            <el-option
              v-for="item in terraces"
              :key="item.value"
              :label="item.name"
              :value="item.value">
            </el-option>
          </el-select>
        </el-col>

        <el-col :span="2" :offset="1" class="elspan martop">
          <span>广告形式：</span>
        </el-col>
        <el-col :span="3" class="martop">
          <el-select v-model="shape" placeholder="请选择">
            <el-option
              v-for="item in shapes"
              :key="item.type"
              :label="item.name"
              :value="item.type">
            </el-option>
          </el-select>
        </el-col>

        <el-col :span="1" :offset="1" class="elspan martop">
          <span>排序：</span>
        </el-col>
        <el-col :span="3" class="martop">
          <el-select v-model="sort" placeholder="请选择">
            <el-option
              v-for="item in sorts"
              :key="item.value"
              :label="item.name"
              :value="item.value">
            </el-option>
          </el-select>
        </el-col>

        <el-col :span="1" :offset='1' class="martop">
          <el-button type="primary" @click="search()">查询</el-button>
        </el-col>
      </el-row>

      <el-row class="rowul">
        <div class="divul">
          <ul class="ultitle">
            <li>adID</li>
            <li>ECPM</li>
          </ul>
          <ul class="ulcontent"
            v-for="(item,index) in ulitem"
            :key="index">
            <li>{{item.AdId}}</li>
            <li>{{item.Ecpm}}</li>
            <li>
              <el-input v-model="item.Num" placeholder="1-100" maxlength="3"></el-input>
              <el-button type="primary" @click="revise(item)" size="small">确认</el-button>
            </li>
            <li>{{item.FinalEcpm}}</li>
          </ul>
          <div class="ulnoinfo" v-show="noinfo">
            <span>暂无数据</span>
          </div>
        </div>
      </el-row>

      <el-row class="pagin" v-if="pagin">
        <el-pagination class="pagin"
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
  </div>
</template>

<script>
  import '../css/nodemonitor.less'
  import '../js/axios.js'
  export default {
    name: "NodeMonitor",
      data () {
        return {
          terrace:'微信',           //支付平台
          terraces:[
            {value:'0',name:'微信'},
            {value:'1',name:'支付宝'},
            {value:'2',name:'其它'},
          ],
          shape:'',               //形式
          shapes:[],
          sort:'TOP',                //排序
          sorts:[
            {value:'1',name:'TOP'},
            {value:'2',name:'ECPM'},
          ],
          ulitem:[],                            //查询列表
          ulitems:[],
          noinfo:false,
          currentPage:1,            //当前页
          pagesize:8,              //每页大小
          total:1,                    //总数
          pagin:false,                    //分页v-if
        }
      },
      methods:{
        GetNodeDetail(){                 //获取节点列表
          var _this=this;
          _this.ulitem=[];
          _this.ulitems=[];
          _this.currentPage=1;
          var flowtype='';
          var sourcetype='';
          var queuetype='';
            if(_this.terrace=='微信'){
              flowtype=0;
            }else{
              flowtype=Number(_this.terrace);
            }
            if(_this.shape=='文字链'){
              sourcetype=2;
            }else{
              sourcetype=Number(_this.shape);
            }
            if(_this.sort=='TOP'){
              queuetype=1;
            }else{
              queuetype=Number(_this.queuetype);
            }

            _this.$http.post('/Admin/NodeMonitor/GetNodeDetail',{
              flowType:flowtype,
              sourceType:sourcetype,
              queueType:queuetype,
            }).then(function(responses){
              var info=responses.data;
              if(info==""||info==null||info==0){
                _this.noinfo=true;
                _this.pagin=false;
              }else{
                var infolist=JSON.parse(info.list);
                if(infolist.length>0||infolist!=null||infolist!=''){
                  if(infolist.length>_this.pagesize){
                    _this.pagin=true;
                  }
                  for (let index = 0; index < infolist.length; index++) {
                    _this.ulitems.push(infolist[index]);
                  }
                  for (let index = 0; index < _this.pagesize; index++) {
                    _this.ulitem.push(_this.ulitems[index]);
                  }
                  _this.total=infolist.length;
                  _this.noinfo=false;
                }else{
                  _this.noinfo=true;
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
        search(){                //查询
          var _this=this;
          _this.GetNodeDetail();
        },
        revise(item){                //修改ECPM值
          var _this=this;
          var regnum = /^[0-9]+([.]{1}[0-9]+){0,1}$/;    //数字正则
          if (!regnum.test(item.Num)) {
              $.message({
                  message: '请输入正确的数值',
                  type: 'warning'
              });
              return;
          } else if (100 <parseFloat(item.Num) || parseFloat(item.Num)< 0) {
              $.message({
                  message: '请输入0~100的数值',
                  type: 'warning'
              });
              return;
          } else {
              var datainfo = {
                  adId: id,
                  num: parseFloat(item.Num)
              };
              _this.$http.post('/Admin/NodeMonitor/SetEcpmNum').then(function(response){
                if(response.data.code=='ok'){
                  
                }
              });
          }
        },
        handleSizeChange(val) {    //分页功能
          this.pagesize=val;
          this.ulitem=[];
          this.currentPage=1;
          if(this.total<val){
            for (let index = 0; index < this.ulitems.length; index++) {
              this.ulitem.push(this.ulitems[index]);
            }
          }else{
            for (let index = 0; index < val; index++) {
              this.ulitem.push(this.ulitems[index]);
            }
          }
          
        },
        handleCurrentChange(val) {    //当前页改变
          var _this=this;
          this.ulitem=[];
          var startPage=this.pagesize*(val-1)+1;
          var endPage=this.pagesize*val;
          var result=0;
          if(this.total/this.pagesize>parseInt(this.total/this.pagesize)){
            result=parseInt(this.total/this.pagesize)+1;
          }else{
            result=parseInt(this.total/this.pagesize);
          }

          //判断最后页的数据够不够整页渲染
          if(val==result){
            for (let index = startPage-1; index < _this.ulitems.length; index++) {
              _this.ulitem.push(_this.ulitems[index]);
            }
          }else{
            for (let index = startPage-1; index <= endPage-1; index++) {
              _this.ulitem.push(_this.ulitems[index]);
            }
          }
        },
      },
      created(){
        var _this=this;
        _this.ulitem=[];
        _this.ulitems=[];
        var flowtype='';
        var sourcetype='';
        var queuetype='';
        this.$http.post('/CreateAd/GetAdSourceList').then(function(response){
          if (response.data!=0||response.data!=null) {
            for(var item in response.data){
              _this.shapes.push(response.data[item]);
            }
            _this.shape=_this.shapes[0].name;
            _this.GetNodeDetail();
          }else{
            _this.shapes=[];
            _this.noinfo=true;
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

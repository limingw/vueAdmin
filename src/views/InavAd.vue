<template>
  <div class="inavad">
    <div v-show="index.index">
      <div class="inainfo">
        <el-row class="rowyed">
          <el-col :span="9" class="elyed">
            <span>昨日访问总量：{{index.yetoday}}</span>
          </el-col>
        </el-row>

        <el-row class="rowtoday">
          <el-col :span="9" class="eltoday">
            <span>今日总访问量：{{index.today}}</span>
          </el-col>

          <el-col :span="5" class="eltrend">     <!--趋势图标-->
            <i class="icon iconfont icon-qushi_shangsheng" v-show="index.rise"></i>
            <i class="icon iconfont icon-ic_trending_flat_px" v-show="index.balance"></i>
            <i class="icon iconfont icon-qushi_xiajiang" v-show="index.drop"></i>
            <span>{{index.differ}}</span>
          </el-col>

            <el-col :span="2" class="elbtn" :offset="1">
              <el-button type="primary" @click="addflows()">添加流量方</el-button>
            </el-col>

            <el-col :span="2" class="elbtn">
              <el-button type="primary" @click="flowsz()">流量方设置</el-button>
            </el-col>

            <el-col :span="2" class="elbtn">
              <el-button type="primary" @click="addinavad()">添加广告</el-button>
            </el-col>

            <el-col :span="2" class="elbtn">
              <el-button type="primary" @click="inavadsz()">广告设置</el-button>
            </el-col>
        </el-row>
      </div>

      <div class="flowinfo">
        <el-row class="rowflowy">
          <el-col :span="5" :offset="3" class="elflowy">
            <span>流量方昨日访问总量：{{index.flowy}}</span>
          </el-col>

          <el-col :span="4" class="elflowy">
            <span>流量方当日总曝光：{{index.exposure}}</span>
          </el-col>

          <el-col :span="5" class="elflowy">
            <span>流量方弹窗当日总曝光：{{index.pop_exposure}}</span>
          </el-col>

          <el-col :span="5" class="elflowurl">
            <span>流量方访问链接</span>
          </el-col>
        </el-row>

        <el-row class="rowflowt">
          <el-col :span="3" class="elsel">
            <el-select v-model="index.select" :placeholder="index.placeh" @change="flwsele()">
              <el-option
                v-for="item in index.selects"
                :key="item.FlowId"
                :label="item.Name"
                :value="item.FlowId">
              </el-option>
            </el-select>
          </el-col>

          <el-col :span="5" class="elflowt">
            <span>流量方今日访问总量：{{index.flowt}}</span>
          </el-col>

          <el-col :span="4" class="elflowy">
            <span>流量方当日总曝光：{{index.clicks}}</span>
          </el-col>

          <el-col :span="5" class="elflowy">
            <span>流量方弹窗当日总曝光：{{index.pop_clicks}}</span>
          </el-col>

          <el-col :span="5" :offset="0" class="elinput">
            <el-input v-model="index.flowurl" id="flwurl" ref="flurl" readonly="readonly"></el-input>
          </el-col>

          <el-col :span="1" class="elcopy">
            <el-button type="primary" :disabled="index.copybtn" @click="copyurl()">复制</el-button>
          </el-col>
        </el-row>

        <!-- <el-row class="underall">
          <el-col :span="2" class="elunder" v-if="index.underbtn">
            <el-button type="primary" @click="underall()">全部下架</el-button>
          </el-col>
        </el-row> -->
      </div>

      <div class="table">
        <el-row>
          <el-table border :data="index.tabledata"
              :default-sort = "{prop: 'StartTime', order: 'descending'}">
                <el-table-column
                  header-align="center"
                  align="center"
                  prop="InavName"
                  label="名字"
                  >
                </el-table-column>
                <el-table-column
                  header-align="center"
                  align="center"
                  prop="StartTime"
                  sortable
                  label="开始时间">
                </el-table-column>
                <el-table-column
                  header-align="center"
                  align="center"
                  prop="EndTime"
                  sortable
                  label="结束时间">
                </el-table-column>
                <el-table-column
                  header-align="center"
                  align="center"
                  prop="RoleDesInfo"
                  label="总量"
                  min-width="100px">
                  <template slot-scope="scope">
                    <p>总点击量：{{scope.row.TotalClickSum}}</p>
                    <p>总曝光量：{{scope.row.TotalShowSum}}</p>
                  </template>
                </el-table-column>
                <el-table-column
                  header-align="center"
                  align="center"
                  prop="StatusDesInfo"
                  label="当日总量"
                  min-width="150px">
                  <template slot-scope="scope">
                    <p>当日总点击量：{{scope.row.DayClickSum}}</p>
                    <p>当日总曝光量：{{scope.row.DayShowSum}}</p>
                  </template>
                </el-table-column>
                <el-table-column
                  header-align="center"
                  align="center"
                  prop="PlatFormType"
                  label="投放类型"
                  :formatter="platFromType">
                </el-table-column>
                <el-table-column
                  header-align="center"
                  align="center"
                  prop="Detial"
                  label="描述">
                </el-table-column>
                <el-table-column
                  header-align="center"
                  align="center"
                  label="操作"
                  width="280px">
                  <template slot-scope="scope">
                    <el-button type="primary" @click="godetail(scope.row)" size="small">详情</el-button>
                    <el-button type="primary" @click="refresh(scope.row)" size="small">更新</el-button>
                    <el-button type="primary" @click="under(scope.row)" size="small">下架</el-button>
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
            :page-sizes="[2, 4, 6]"
            :page-size="index.pagesize"
            :pager-count="5"
            layout="total, sizes, prev, pager, next, jumper"
            :total="index.total">
          </el-pagination>
        </el-row>
      </div>
    </div>

    <!--流量方设置-->
    <div v-if="addflow.index" class="flowad">
      <el-row class="rowflname">
        <el-col :span="3" :offset="6" class="elname">
          <span>流量方名称：</span>
        </el-col>

        <el-col :span="7" :offset="0">
          <el-input v-model="addflow.flname" maxlength="50" placeholder="请输入流量方名称" ref="flnames" clearable></el-input>
        </el-col>
      </el-row>

      <el-row class="rowflbtn">
        <el-col :span="2" :offset="8">
          <el-button type="primary" @click="cancel()">取消</el-button>
        </el-col>

        <el-col :span="2" :offset="3">
          <el-button type="primary" @click="fladdbtn()">确认</el-button>
        </el-col>
      </el-row>
    </div>

    <div v-show="flowsetup.index" class="setuptitle">
      <el-row class="rowsetuptitle">
        <el-col :span="4" :offset="4" class="elspan">
          <span>屏蔽广告品类</span>
        </el-col>

        <el-col :span="2" class="seletupback">
          <el-button type="primary" @click="setupback()">返回首页</el-button>
        </el-col>
      </el-row>

      <transition-group name="slide">
        <el-row class="rowselect" 
        v-for="(item,index) in flowsetup.selectitem" 
        :key="index">
        <el-col :span="4" :offset="4">
          <el-select @change="selectchange(item.type_id,index)" v-model="item.list1" placeholder="请选择">
            <el-option
              v-for="item in item.classlist"
              :key="item.type_id"
              :label="item.type_name"
              :value="item.type_id">
            </el-option>
          </el-select>
        </el-col>
        
        <el-col :span="4" :offset="1">
          <el-select v-model="item.list2"  placeholder="请选择">
            <el-option
              v-for="item in item.jsonlist"
              :key="item.id"
              :label="item.name"
              :value="item.id">
            </el-option>
          </el-select>
        </el-col>
        
        <el-col :span="2" :offset="1">
          <el-button type="primary" @click="deletesecelt(index)">删除</el-button>
        </el-col>
      </el-row>
      </transition-group>
      

      <el-row class="rowlastbtn">
        <el-col :span="2" :offset="4">
          <el-button type="primary" @click="addselect()">增加</el-button>
        </el-col>
        <el-col :span="2" :offset="6">
          <el-button type="primary" @click="setupcommit()">提交</el-button>
        </el-col>
      </el-row>
    </div>

    <!--添加广告-->
    <div v-show="addinav.index" class="addinavad">
      <el-row class="rowinavname">
        <el-col :span="2" :offset="7" class="elnamespan">
          <el-tag class="tags">名字：</el-tag>
        </el-col>
        <el-col :span="7" class="elname">
          <el-input placeholder="请输入名字" ref="addname" v-model="addinav.name" clearable>
          </el-input>
        </el-col>

        <el-col :span="2" :offset="1" class="elselect">
          <el-select v-model="addinav.select">
            <el-option
              v-for="item in addinav.selects"
              :key="item.value"
              :label="item.name"
              :value="item.value">
            </el-option>
          </el-select>
        </el-col>
      </el-row>

      <el-row class="rowinavremark">
        <el-col :span="2" :offset="7" class="elscript">
          <el-tag class="tags">描述：</el-tag>
        </el-col>
        <el-col :span="7">
          <el-input type="textarea"
            :autosize="{ minRows: 8}"
            placeholder="请输入内容"
            v-model="addinav.discript"
            ref="addremark">
          <template slot="prepend">名字：</template>
          </el-input>
        </el-col>

        <el-col :span="2" :offset="1" class="elselect">
          <el-select v-model="addinav.typeitem">
            <el-option
              v-for="item in addinav.typeitems"
              :key="item.value"
              :label="item.name"
              :value="item.value">
            </el-option>
          </el-select>
        </el-col>
      </el-row>

      <el-row class="rowinavmatrial">
        <el-col :span="2" :offset="7">
          <el-button type="info">页面素材</el-button>
        </el-col>

        <el-col :span="4" :offset="1">
          <el-input class="ke-input-text" type="text" v-model="addinav.material" id="inavAdResourceUrl" value="" readonly="readonly" ></el-input>
        </el-col>

        <el-col :span="2" :offset="1" class="elupfile">
          <el-upload
            class="upload-demo"
            action="/ToolFiles/kindeditor/asp.net/inavAdFile_upload.ashx?dir=file"
            :on-success="addfilesuccess"
            :show-file-list="false"
            list-type="text"
            name="imgFile"
            accept=".zip,.rar">
            <el-button size="small" type="primary">点击上传</el-button>
          </el-upload>
        </el-col>
      </el-row>

      <el-row class="rowinavbtn">
        <el-col :span="2" :offset="7">
          <el-button type="primary" @click="addinavback()">取消</el-button>
        </el-col>

        <el-col :span="2" :offset="5" class="elsure">
          <el-button type="primary" @click="addinavsure()">确认</el-button>
        </el-col>
      </el-row>
    </div>

    <!--广告设置-->
    <div v-show="inavsetup.index" class="inavsetup">
      <el-row class="rowbackbtn">
        <el-col :span="2" class="seletupback">
          <el-button type="primary" @click="setupback()">返回首页</el-button>
        </el-col>
      </el-row>

      <el-row class="inavsetuptable">
        <el-table border 
            :data="inavsetup.tabledata"
            :default-sort= "{prop:'InavPath',order:'descending'}">
            <el-table-column
              header-align="center"
              align="center"
              prop="InavId"
              label="ID">
            </el-table-column>
            <el-table-column 
              header-align="center"
              align="center"
              prop="InavName"
              label="名字"
              sortable>
            </el-table-column>
            <el-table-column
              header-align="center"
              align="center"
              label="开始时间-结束时间"
              class-name="timesinput"
              min-width="360px">
              <template slot-scope="scope">
                <el-input v-model="scope.row.StartTime" placeholder="0~23" maxlength="2" ></el-input>
                <el-input v-model="scope.row.EndTime" placeholder="1~24" maxlength="2"></el-input>
                <el-button type="primary" @click="rechange(scope.row,'time')" size="small">修改</el-button>
              </template>
            </el-table-column>
            <el-table-column 
              header-align="center"
              align="center"
              label="权重"
              class-name="wlinput"
              min-width="302px">
              <template slot-scope="scope">
                <el-input v-model="scope.row.InavWeight" placeholder="请输入权重" maxlength="2"></el-input>
                <el-button type="primary" @click="rechange(scope.row,'weight')" size="small">修改</el-button>
              </template>
            </el-table-column>
            <el-table-column
              header-align="center"
              align="center"
              label="广告个数"
              class-name="wlinput"
              min-width="302px">
              <template slot-scope="scope">
                <el-input v-model="scope.row.InavAdLength" placeholder="请输入广告个数" maxlength="2"></el-input>
                <el-button type="primary" @click="rechange(scope.row,'length')" size="small">修改</el-button>
              </template>
            </el-table-column>
          </el-table>
      </el-row>

      <el-row class="pagin" v-if="inavsetup.pagin">
        <el-pagination class="pagin"
            @size-change="handleSizeChanges"
            @current-change="handleCurrentChanges"
            :current-page.sync="inavsetup.currentPage"
            :page-sizes="[2, 4, 6,8,10]"
            :page-size="inavsetup.pagesize"
            :pager-count="5"
            layout="total, sizes, prev, pager, next, jumper"
            :total="inavsetup.total">
        </el-pagination>
      </el-row>
    </div>

    <!--编辑广告-->
    <div v-show="inavupdate.index" class="addinavad">
      <el-row class="rowinavname">
        <el-col :span="2" :offset="7" class="elnamespan">
          <el-tag class="tags">名字：</el-tag>
        </el-col>
        <el-col :span="7" class="elname">
          <el-input placeholder="请输入名字" readonly ref="editname" v-model="inavupdate.name" clearable>
          </el-input>
        </el-col>

        <el-col :span="2" :offset="1" class="elselect">
          <el-select v-model="inavupdate.select">
            <el-option
              v-for="item in inavupdate.selects"
              :key="item.value"
              :label="item.name"
              :value="item.value">
            </el-option>
          </el-select>
        </el-col>
      </el-row>

      <el-row class="rowinavremark">
        <el-col :span="2" :offset="7" class="elscript">
          <el-tag class="tags">描述：</el-tag>
        </el-col>
        <el-col :span="7">
          <el-input type="textarea"
            :autosize="{ minRows: 12}"
            placeholder="请输入内容"
            ref="editremark"
            v-model="inavupdate.discript">
          <template slot="prepend">名字：</template>
          </el-input>
        </el-col>

        <el-col :span="2" :offset="1" class="elselect">
          <el-select v-model="inavupdate.typeitem">
            <el-option
              v-for="item in inavupdate.typeitems"
              :key="item.value"
              :label="item.name"
              :value="item.value">
            </el-option>
          </el-select>
        </el-col>
      </el-row>

      <el-row class="rowinavmatrial">
        <el-col :span="2" :offset="7" class="elupfile">
          <el-button type="info">页面素材</el-button>
        </el-col>

        <el-col :span="4" :offset="1">
          <el-input class="ke-input-text" type="text" v-model="inavupdate.material" id="inavAdResourceUrl" value="" readonly="readonly" ></el-input>
        </el-col>

        <el-col :span="2" :offset="1">
          <el-upload
            class="upload-demo"
            :action="inavupdate.actionpath"
            :on-success="updatefilesuccess"
            :show-file-list="false"
            list-type="text"
            name="imgFile"
            accept=".zip">
            <el-button size="small" type="primary">点击上传</el-button>
          </el-upload>
        </el-col>
      </el-row>

      <el-row class="rowinavbtn">
        <el-col :span="2" :offset="7">
          <el-button type="primary" @click="addinavback()">取消</el-button>
        </el-col>

        <el-col :span="2" :offset="5" class="elsure">
          <el-button type="primary" @click="inavupdatesure()">确认</el-button>
        </el-col>
      </el-row>
    </div>

    <!--广告详情-->
    <div class="details" v-show="details.index">
        <el-row class="searchtime">
          <el-col :span="2" class="eltag">
            <el-tag type="info" class="tag">注册日期：</el-tag>
          </el-col>
          <el-col :span="8" class="elinput" :offset="1">
            <el-date-picker
              v-model="details.time"
              type="daterange"
              align="left"
              unlink-panels
              range-separator="至"
              start-placeholder="开始日期"
              end-placeholder="结束日期"
              :picker-options="pickerOptions"
              format="yyyy 年 MM 月 dd 日"
              value-format="yyyy/MM/dd">
            </el-date-picker>
          </el-col>
          <el-col :span="2" :offset="1">
            <el-button type="primary" @click="seartime()">查询</el-button>
          </el-col>
          <el-col :span="2" :offset="1">
            <el-button type="primary" @click="reset()">重置</el-button>
          </el-col>
          <el-col :span="2" class="eldetailre">
            <el-button type="primary" @click="rebackindex()">返回首页</el-button>
          </el-col>
        </el-row>

        <el-row class="detailtable">
        <el-table border 
            :data="details.tabledata"
            :default-sort = "{prop: 'RecordTime', order: 'descending'}">
            <el-table-column
              header-align="center"
              align="center"
              prop="RecordTime"
              sortable
              label="日期">
            </el-table-column>
            <el-table-column 
              header-align="center"
              align="center"
              prop="AmountOfAccess"
              sortable
              label="访问量">
            </el-table-column>
            <el-table-column
              header-align="center"
              align="center"
              prop="ShowTimes"
              sortable
              label="曝光量">
            </el-table-column>
            <el-table-column 
              header-align="center"
              align="center"
              prop="ClickTimes"
              sortable
              label="点击量">
            </el-table-column>
            <el-table-column
              header-align="center"
              align="center"
              prop="ClickingRate"
              sortable
              label="点击率">
            </el-table-column>
          </el-table>
      </el-row>

      <el-row class="pagin" v-show="details.pagin">
        <el-pagination class="pagin"
            @size-change="detailSizeChange"
            @current-change="detailCurrentChange"
            :current-page.sync="details.currentPage"
            :page-sizes="[2, 4, 6,8,10]"
            :page-size="details.pagesize"
            :pager-count="5"
            layout="total, sizes, prev, pager, next, jumper"
            :total="details.total">
        </el-pagination>
      </el-row>
    </div>
  </div>
</div>
  
</template>


<script>
  import '../css/inavad.less'
  import '../js/axios.js'
  export default {
    name: "InavAd",
    data () {
      return {
        index:{             //主页
          index:true,
          yetoday:0,           //昨日访问总量
          today:0,             //今日访问总量
          differ:0,            //相差访问量
          rise:false,             //趋势图标
          balance:true,
          drop:false,
          select:'',              //广告类型下拉框
          selects:[],
          placeh:'请选择',  
          flowy:0,             //流量方昨日访问总量
          flowt:0,               //流量方今日访问总量
          exposure:0,                  //总曝光
          pop_exposure:0,              //弹窗总曝光
          clicks:0,                     //总点击
          pop_clicks:0,                 //弹窗总点击
          flowurl:'',               //流量方访问链接
          copybtn:false,           //复制按钮禁用
          underbtn:true,            //全部下架按钮
          tabledata:[],
          currentPage:1,            //当前页
          pagesize:6,              //每页大小
          total:1,                    //总数
          pagin:true,                    //分页v-if
        },
        addflow:{                 //添加流量方
          index:false,
          flname:'',          
        },
        flowsetup:{                       //流量方设置
          index:false,
          classlist:[],                   //品类1
          list1:'',
          jsonlistinfo:[],
          jsonlist:[],                     //品类2
          list2:'',
          selectitem:[],
          additem:0,
          result:[],
        },
        addinav:{                        //添加广告
          index:false,
          name:'',                //名字
          discript:'',              //描述
          select:'1',              //广告类型下拉框
          selects:[
            {value:'1',name:'小图'},
            {value:'4',name:'Banner大图'},
            {value:'10',name:'WIFI-Ⅰ'},
            {value:'13',name:'BANNER_4'},
            {value:'15',name:'支付完成Ⅰ'}
          ],
          typeitem:'0',              //投放类型
          typeitems:[
            {value:'0',name:'通投'},
            {value:'1',name:'支付宝'},
            {value:'2',name:'微信'},
            {value:'3',name:'其它'}
          ],
          material:'',               //素材
          inavAdResourceUrl:'',              //上传素材
        }, 
        inavsetup:{                  //广告设置
          index:false,        
          tabledata:[],
          startime:'',
          endtime:'',
          path:'',
          number:'',   
          currentPage:1,            //当前页
          pagesize:8,              //每页大小
          total:1,                    //总数
          pagin:false,                    //分页v-if
        },
        inavupdate:{                  //更新广告                    
          index:false,
          name:'',
          discript:'',
          select:'1',              //广告类型下拉框
          selects:[
            {value:'1',name:'小图'},
            {value:'4',name:'Banner大图'},
            {value:'10',name:'WIFI-Ⅰ'},
            {value:'13',name:'BANNER_4'},
            {value:'15',name:'支付完成Ⅰ'}
          ],
          typeitem:'0',              //投放类型
          typeitems:[
            {value:'0',name:'通投'},
            {value:'1',name:'支付宝'},
            {value:'2',name:'微信'},
            {value:'3',name:'其它'}
          ],
          inavadid:'',
          material:'',               //素材
          inavAdResourceUrl:'',              //上传素材
          actionpath:'/ToolFiles/kindeditor/asp.net/inavAdFile_upload.ashx?dir=file&inavId=',               //文件上传地址
        },
        details:{                    //广告详情
          index:false,
          time:'',
          tabledata:[],
          inavadid:'',
          currentPage:1,            //当前页
          pagesize:8,              //每页大小
          total:1,                    //总数
          pagin:false,                    //分页v-if
        },
        pickerOptions: {       //日期快捷选择
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
      GetAdClassList() {                 //获取广告品类
          var _this=this;
            let classList = sessionStorage.getItem('typeData');
            if (classList === null || classList === '') {
              _this.$http.post('/CreateAd/GetAdClassList').then(function(response){
                sessionStorage.setItem('typeData', JSON.stringify(response.data));
              }).catch(function(error){
                console.loc(error);
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
      getinavAdmgLists(currentpage,pagesize,flowid){    //获取广告信息
        var _this=this;
        _this.index.tabledata=[];
        if(flowid==''){
          _this.index.tabledata=[];
          _this.index.total=0;
        }else{
          _this.$http.get('/Admin/InavAd/GetInavAdMgLists',{
            params:{
              pageNo:currentpage,
              pageSize:pagesize,
              flowId:flowid
            }
          }).then(function(response){
            var info=response.data;
            if(info!=''||info!=null){
              _this.index.total=info.TotalCount;
              _this.index.tabledata=info.InavAdModels;
              // _this.index.today=info.TodayTraffice;
              // _this.index.yetoday=info.YesterdayTraffic;
              // _this.index.differ=_this.index.today-_this.index.yetoday;
            }else{
              _this.index.total=0;
              _this.index.today=0;
              _this.index.yetoday=0;
              _this.index.tabledata=[];
            }

            if(_this.index.total<=0){
              _this.index.pagin=false;
            }else{
              _this.index.pagin=true;
            }

            if(_this.index.today>_this.index.yetoday){      //判断趋势图标
              _this.index.differ=_this.index.today-_this.index.yetoday;
              _this.index.rise=true;
              _this.index.balance=false;
              _this.index.drop=false;
            }else if(_this.index.today==_this.index.yetoday){
              _this.index.differ=_this.index.today-_this.index.yetoday;
              _this.index.rise=false;
              _this.index.balance=true;
              _this.index.drop=false;
            }else if(_this.index.today<_this.index.yetoday){
              _this.index.differ=_this.index.yetoday-_this.index.today;
              _this.index.rise=false;
              _this.index.balance=false;
              _this.index.drop=true;
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
      GetdetailInfo(currentpage,pagesize,inavid,stime,endtime){       //获取广告详情信息
        var _this=this;
        _this.details.tabledata=[];
        var params={};
        if(stime==''||endtime==''){
          params={
            pageNo:currentpage,
            pageSize:pagesize,
            inavId:inavid,
            startTime:stime,
            endTime:endtime
          };
        }else{
          params={
            pageNo:currentpage,
            pageSize:pagesize,
            inavId:inavid,
            startTime:stime,
            endTime:endtime
          };
        }

        _this.$http.get('/Admin/InavAd/GetInavAdDailyMgLists',{
          params:params
        }).then(function(response){
          var info=response.data;
          if(info!=''||info!=null){
            _this.details.total=info.TotalCount;
            _this.details.tabledata=info.InavAdDailyModels;
          }else{
            _this.details.total=0;
            _this.details.tabledata=[];
          }

          if(_this.details.total<=0){
            _this.details.pagin=false;
          }else{
            _this.details.pagin=true;
          }
        });
      },
      GetInavAdmglists(currentpage,pagesize){             //获取广告设置列表
        var _this=this;
        var flowid='';
        for (let index = 0; index < _this.index.selects.length; index++) {
          if(_this.index.select==_this.index.selects[index].Name){
            flowid=_this.index.selects[index].FlowId;
          }else{
            flowid=_this.index.select;
          }
        }
        _this.$http.post('/Admin/InavAd/GetInavAdMgLists',{
          pageNo:currentpage,
          pageSize:pagesize,
          flowId:flowid
        }).then(function(response){
          var info=response.data;
          if(info!=''||info!=null){
            _this.inavsetup.total=info.TotalCount;
            _this.inavsetup.tabledata=info.InavAdModels;
          }else{
            _this.inavsetup.total=0;
            _this.inavsetup.tabledata=[];
          }

          if(_this.inavsetup.total<=0){
            _this.inavsetup.pagin=false;
          }else{
            _this.inavsetup.pagin=true;
          }
        }).catch(function(error){
          console.log(error);
          _this.$message({
            message:'服务器错误',
            type:'error'
          });
        });
      },
      GetDailySum(){                             //获取首页访问量信息
        var _this=this;
        _this.$http.post('/Admin/InavAd/GetDailySum').then(function(response){
          var info=response.data;
          if(info!=''||info!=null){
            _this.index.today=info.sums.Item1;
            _this.index.yetoday=info.sums.Item2;
            if(_this.index.today>_this.index.yetoday){
              _this.index.differ=info.sums.Item1-info.sums.Item2;
              _this.index.rise=true;
            }else if(_this.index.today===_this.index.yetoday){
              _this.index.differ=info.sums.Item1-info.sums.Item2;
              _this.index.balance=true;
            }else if(_this.index.today<_this.index.yetoday){
              _this.index.differ=info.sums.Item2-info.sums.Item1;
              _this.index.drop=true;
            }
          }else{
            _this.index.today=0;
            _this.index.yetoday=0;
            _this.index.differ=0;
            _this.index.balance=true;
          }
        }).catch(function(error){
          console.log(error);
          _this.$message({
            message:'服务器错误',
            type:'error'
          });
        });
      },
      GetTodayInfo(){                     //获取点击和曝光
        var _this=this;
        _this.$http.post('/Admin/InavAd/GetAllShowAndClick').then(function(response){
          var info=response.data;
          _this.index.exposure=info.ShowTimes;
          _this.index.clicks=info.ClickTimes;
          _this.index.pop_exposure=info.PopWinShowTimes;
          _this.index.pop_clicks=info.PopWinClickTimes;
        }).catch(function(error){
          console.log(error);
          _this.$message({
            message:'服务器错误',
            type:'error'
          });
        });
      },
      seartime(){       //查询时间按钮
        var _this=this;
        if(this.details.time==''){
          this.$message({
						message: '请输入要查询的时间段',
						type: 'warning'
          });
          return;
        }else{
          var currentpage=_this.details.currentPage;
          var pagesize=_this.details.pagesize;
          var inavid=_this.details.inavadid;
          var stime=_this.details.time[0].replace('/','-');
          var endtime=_this.details.time[1].replace('/','-');
          _this.GetdetailInfo(currentpage,pagesize,inavid,stime,endtime);
        }
      },
      reset(){                     //广告详情重置
        var _this=this;
        _this.details.time='';
        _this.GetdetailInfo(_this.details.currentPage,_this.details.pagesize,_this.details.inavadid,'','');
      },
      addflows(){         //添加流量方
        this.index.index=false;
        this.addflow.index=true;
        this.addflow.flname='';
      },
      flowsz(){             //流量方设置
        if(this.index.select==''){
          this.$message({
            message:'请先添加流量方',
            type:'warning'
          });
          return;
        }
        var _this=this;
        this.index.index=false;
        this.flowsetup.index=true;
        this.flowsetup.selectitem=[];
        _this.flowsetup.classlist=[];
        _this.flowsetup.jsonlist=[];
        //广告品类
        let Adlists = JSON.parse(sessionStorage.getItem('typeData'));
        for (let index = 0; index < this.index.selects.length; index++) {
          if(this.index.select==this.index.selects[index].Name){
            this.index.select=this.index.selects[index].FlowId;
          }
        }

          var list1info=Adlists.classList;
          var list2info=Adlists.jsonList;
          if(list1info!=0){
            for (let index = 0; index < list1info.length; index++) {
              _this.flowsetup.classlist.push(list1info[index].Data);
            }
            _this.flowsetup.list1=_this.flowsetup.classlist[0].type_name;
          }else{
            _this.flowsetup.list1='暂无品类';
          }

          if(list2info!=0){
            for (let index = 0; index < list2info.length; index++) {
              _this.flowsetup.jsonlistinfo.push(list2info[index].Data);
            }
            for (let index = 0; index < 11; index++) {
              _this.flowsetup.jsonlist.push(_this.flowsetup.jsonlistinfo[index]);
            }
          }else{
            _this.flowsetup.list2='暂无品类';
          }
      },
      platFromType(row,column,cellValue){           //投放类型判断
        if(row.PlatFromType===''||row.PlatFromType===null){
          return '/';
        }else{
          if(row.PlatFromType==0){
            return '通投';
          }else if(row.PlatFromType==1){
            return '支付宝';
          }else if(row.PlatFromType==2){
            return '微信';
          }else if(row.PlatFromType==3){
            return '其它';
          }
        }
      },
      addinavad(){            //添加广告
        if(this.index.select==''){
          this.$message({
            message:'请先添加流量方',
            type:'warning'
          });
          return;
        }
        this.addinav.index=true;
        this.index.index=false;
        this.addinav.name='';
        this.addinav.discript='';
        this.addinav.select='1';
        this.addinav.inavAdResourceUrl='';
      },
      addinavback(){              //添加广告返回
        this.addinav.index=false;
        this.index.index=true;
        this.inavupdate.index=false;
        if(this.addinav.select=='小图'){
          this.addinav.select=1;
        }
      },
      addinavsure(){              //添加广告确认
        var _this=this;
        var repeat=false;     //用于防止多次点击
        if(!repeat){
          if(_this.addinav.name==''){
            _this.$message({
              message:'请输入广告名称',
              type:'warning'
            });
            _this.$refs.addname.focus();
            return;
          }else if(_this.addinav.discript==''){
            _this.$message({
              message:'请输入广告描述',
              type:'warning'
            });
            _this.$refs.addremark.focus();
            return;
          }else if(_this.addinav.select==''){
            _this.$message({
              message:'请选择广告类型',
              type:'warning'
            });
            return;
          }else if(_this.addinav.typeitem==''){
            _this.$message({
              message:'请选择投放类型',
              type:'warning'
            });
            return;
          }else if(_this.addinav.inavAdResourceUrl==''){
            _this.$message({
              message:'请上传广告素材',
              type:'warning'
            });
            return;
          }else{
            repeat=true;
            var info={
              inavAdName:_this.addinav.name,
              inavAdDetail:_this.addinav.discript,
              inavadPath:_this.addinav.inavAdResourceUrl,
              flowId:_this.index.select,
              type:_this.addinav.select,
              platformType:_this.addinav.typeitem
            };
            _this.$http.post('/Admin/InavAd/AddInavAd',info).then(function(response){
              var info=response.data;
              if(info=='3003'){
                _this.$message({
                  message:'数据不能为空',
                  type:'warning'
                });
              }else if(info=='3005'){
                _this.$message({
                  message:'描述字数过长(180字以内哦~)',
                  type:'warning'
                });
              }else if(info=='3004'){
                _this.$message({
                  message:'广告已存在',
                  type:'warning'
                });
              }else{
                _this.$message({
                  message:'添加成功',
                  type:'success'
                });
                setTimeout(function(){
                  _this.index.index=true;
                  _this.addinav.index=false;
                  _this.getinavAdmgLists(_this.index.currentPage,_this.index.pagesize,_this.index.select);
                },1500);
              }
              repeat=false;
            });
          }
        }else{
          return;
        }
        
      },
      inavupdatesure(){              //更新广告确认
        var _this=this;
        if(_this.inavupdate.name==''){
          _this.$message({
            message:'请输入描述',
            type:'warning'
          });
          _this.$refs.editname.focus();
          return;
        }else if(_this.inavupdate.discript==''){
          _this.$message({
            message:'请输入描述',
            type:'warning'
          });
          _this.$refs.editremark.focus();
        }else if(_this.index.select==''){
          _this.$message({
            message:'请先选择流量方',
            type:'warning'
          });
          return;
        }else if(_this.inavupdate.inavAdResourceUrl==''){
          _this.$message({
            message:'请选择要更新的广告素材',
            type:'warning'
          });
          return;
        }else{
          for (let index = 0; index < _this.inavupdate.selects.length; index++) {
            if(_this.inavupdate.selects[index].name==_this.inavupdate.select){
              _this.inavupdate.select=_this.inavupdate.selects[index].value;
            }
            
          }
          for (let index = 0; index < _this.inavupdate.typeitems.length; index++) {
            if(_this.inavupdate.typeitems[index].name==_this.inavupdate.typeitem){
              _this.inavupdate.typeitem=_this.inavupdate.typeitems[index].value;
            }
            
          }
          var info={
            inavId:_this.inavupdate.inavadid,
            name:_this.inavupdate.name,
            detail:_this.inavupdate.discript,
            path:_this.inavupdate.inavAdResourceUrl,
            flowId:_this.index.select,
            type:_this.inavupdate.select,
            platformType:_this.inavupdate.typeitem
          };
          _this.$http.post('/Admin/InavAd/ModifyInavAdResource',info).then(function(response){
            if(!response.data){
              _this.$message({
                message:'更新失败',
                type:'error'
              });
              return;
            }else if(response.data=='0101'){
              _this.$message({
                message:'描述内容未改变',
                type:'warning'
              });
              _this.$refs.editremark.focus();
              return;
            }else{
              _this.$message({
                message:'更新成功',
                type:'success'
              });
              setTimeout(function(){
                _this.inavupdate.index=false;
                _this.index.index=true;
                _this.index.pagin=true;
                _this.getinavAdmgLists(_this.index.currentPage,_this.index.pagesize,_this.index.select);
              },1500);
            }
          });
        }
      },
      inavadsz(){              //广告设置
        if(this.index.select==''){
          this.$message({
            message:'请先添加流量方',
            type:'warning'
          });
          return;
        }
        var _this=this;
        this.inavsetup.index=true;
        this.index.index=false;
        this.index.pagin=false;
        this.inavsetup.pagin=true;
        console.log(_this.index.select);
        var currentpage=_this.inavsetup.currentPage;
        var pagesize=_this.inavsetup.pagesize;
        _this.GetInavAdmglists(currentpage,pagesize);
      },
      rechange(row,types){                   //修改权重时间和长度
        var _this=this;
        var Reg = /(^[1-9]([0-9]+)?(\.[0-9]{1,2})?$)|(^(0){1}$)|(^[0-9]\.[0-9]([0-9])?$)/;
        if(types=='time'){                 //修改时间
          var startTime=row.StartTime;
          var endTime=row.EndTime;
          console.log(typeof(startTime));
          if(startTime===''){
            _this.$message({
              message:'请输入开始时间',
              type:'warning'
            });
            return;
          }else if(endTime===''){
            _this.$message({
              message:'请输入结束时间',
              type:'warning'
            });
            return;
          }else{
            if(!Reg.test(startTime)){
              _this.$message({
                message:'请输入正确的开始时间',
                type:'warning'
              });
              return;
            }else if(!Reg.test(endTime)){
              _this.$message({
                message:'请输入正确的结束时间',
                type:'warning'
              });
              return;
            }else if(parseInt(startTime)<0||parseInt(startTime)>23){
              _this.$message({
                message:'开始时间应该在0~23之间',
                type:'warning'
              });
              return;
            }else if(parseInt(endTime)<0||parseInt(endTime)>24){
              _this.$message({
                message:'结束时间应该在1~24之间',
                type:'warning'
              });
              return;
            }else if(parseInt(endTime)<=parseInt(startTime)){
              _this.$message({
                message:'结束时间需要大于开始时间',
                type:'warning'
              });
              return;
            }else{
              var times={
                inavId:row.InavId,
                startTime:startTime,
                endTime:endTime
              };
              _this.$http.post('/Admin/InavAd/ChangeStartAndEndTime',times).then(function(response){
                if(response.data.status=='ok'){
                  _this.$message({
                    message:response.data.info,
                    type:'success'
                  });
                }else{
                  _this.$message({
                    message:response.data.info,
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
          }
        }else if(types=='weight'){          //修改权重
          if(row.InavWeight===''){
            _this.$message({
              message:'请输入权重',
              type:'warning'
            });
            return;
          }else {
            var weights={
              inavId:row.InavId,
              inavWeight:parseInt(row.InavWeight)
            }
            _this.$http.post('/Admin/InavAd/ChangeWeight',weights).then(function(response){
              var data=response.data;
              if(data.status==4001){
                _this.$message({
                  message:'数据过大',
                  type:'warning'
                });
                return;
              }else if(data.status==4002){
                _this.$message({
                  message:'数据未改变',
                  type:'warning'
                });
                return;
              }else if(data.status==4003){
                _this.$message({
                  message:'服务器错误',
                  type:'warning'
                });
                return
              }else{
                _this.$message({
                  message:'修改成功',
                  type:'success'
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
        }else if(types=='length'){          //修改广告个数
          if(row.InavAdLength===''){
            _this.$message({
              message:'请输入广告个数',
              type:'warning'
            });
            return;
          }else {
            var lengths={
              inavId:row.InavId,
              inavLength:parseInt(row.InavAdLength)
            }
            _this.$http.post('/Admin/InavAd/ChangeLength',lengths).then(function(response){
              var data=response.data;
              if(data.status==4001){
                _this.$message({
                  message:'数据过大',
                  type:'warning'
                });
                return;
              }else if(data.status==4002){
                _this.$message({
                  message:'数据未改变',
                  type:'warning'
                });
                return;
              }else if(data.status==4003){
                _this.$message({
                  message:'服务器错误',
                  type:'warning'
                });
                return
              }else{
                _this.$message({
                  message:'修改成功',
                  type:'success'
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
        }
      },
      copyurl(){                 //复制流量方访问链接
        var _this=this;
          var url=document.querySelector('#flwurl');
          url.select();
          if(document.execCommand('Copy')){
            document.execCommand('Copy');
            this.$message({
              message:'复制成功',
              type:'success'
            });
          }
      },
      underall(){                   //全部下架
        var _this=this;
        var flowid='';
        if(_this.index.flowurl==''||_this.index.flowurl=='请选择流量方'){
          _this.$message({
            message:'请先选择流量方',
            type:'warning',
          });
          return;
        }else{
          for (let index = 0; index < _this.index.selects.length; index++) {
            if(_this.index.select==_this.index.selects[index].Name){
              flowid=_this.index.selects[index].FlowId;
            }else{
              flowid=_this.index.select;
            }
          }
          
          this.$confirm('此操作将下架该流量方所有广告, 是否继续?', '提示', {
            confirmButtonText: '确定',
            cancelButtonText: '取消',
            type: 'warning'
          }).then(() => {
            _this.$http.post('/Admin/InavAd/DisableInavAd',{
              inavadId:flowid,
              idStyle:1
            }).then(function(response){
              if(response.data.statusCode==1011){
                _this.$message({
                  message:'下架成功',
                  type:'success'
                });
              }else{
                _this.$message({
                  message:'下架失败',
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
          }).catch(() => {
            this.$message({
              type: 'info',
              message: '已取消'
            });          
          });
          
        }
      },
      godetail(row){             //广告详情
        var _this=this;
        _this.details.index=true;
        _this.index.index=false;
        _this.index.pagin=false;
        _this.details.inavadid=row.InavId;
        var currentpage=_this.details.currentPage;
        var pagesize=_this.details.pagesize;
        var inavid=_this.details.inavadid;
        _this.GetdetailInfo(currentpage,pagesize,inavid,'','');
      },
      rebackindex(){              //广告详情返回
        var _this=this;
        _this.index.index=true;
        _this.index.pagin=true;
        _this.details.index=false;
        _this.details.pagin=false;
      },
      refresh(row){            //广告更新
        var _this=this;
        _this.inavupdate.index=true;
        _this.index.index=false;
        _this.index.pagin=false;
        _this.flowsetup.pagin=false;
        _this.details.pagin=false;
        _this.inavupdate.inavadid=row.InavId;
        _this.inavupdate.name=row.InavName;
        _this.inavupdate.discript=row.Detial;
        _this.inavupdate.actionpath='/ToolFiles/kindeditor/asp.net/inavAdFile_upload.ashx?dir=file&inavId='+row.InavId;
        
        if(row.InavPath!=''){
          var pathname=row.InavPath.substring(row.InavPath.lastIndexOf('/'));
          _this.inavupdate.material=pathname.replace('/','');
        }else{
          _this.inavupdate.material='';
        }

        _this.inavupdate.select=row.Type;
        for (let index = 0; index < _this.inavupdate.selects.length; index++) {
          if(_this.inavupdate.selects[index].value==row.Type){
            _this.inavupdate.select=_this.inavupdate.selects[index].name;
          }
        }
        if(row.PlatFromType!=null){
          _this.inavupdate.typeitem=row.PlatFromType;
        }else{
          _this.inavupdate.typeitem='0';
        }
        
      },
      under(row){               //单个广告下架
        var _this=this;
        var flowid='';
        for (let index = 0; index < _this.index.selects.length; index++) {
            if(_this.index.select==_this.index.selects[index].Name){
              flowid=_this.index.selects[index].FlowId;
            }else{
              flowid=_this.index.select;
            }
        }
        this.$confirm('此操作将下架该广告, 是否继续?', '提示', {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning'
        }).then(() => {
          _this.$http.post('/Admin/InavAd/DisableInavAd',{
            inavadId:flowid,
            idStyle:0
          }).then(function(response){
            if(response.data.statusCode==1011){
              _this.$message({
                message:'下架成功',
                type:'success'
              });
            }else{
              _this.$message({
                message:'下架失败',
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
        }).catch(() => {
          this.$message({
            type: 'info',
            message: '已取消'
          });          
        });
      },
      flwsele(){                   //流量方下拉框
        var _this=this;
        for (let index = 0; index < _this.index.selects.length; index++) {
          if(_this.index.select==_this.index.selects[index].FlowId){
            _this.index.flowy=_this.index.selects[index].LastPv;
            _this.index.flowt=_this.index.selects[index].CurPv;
            _this.index.flowurl=_this.index.selects[index].Link;
          }
        }
        if(_this.index.flowurl==''||_this.index.flowurl=='请选择流量方'){
          _this.index.copybtn=true;
        }else{
          _this.index.copybtn=false;
        }

        var currentpage=_this.index.currentPage;
        var pagesize=_this.index.pagesize;
        var flowid=_this.index.select;
        _this.getinavAdmgLists(currentpage,pagesize,flowid);
      },
      cancel(){                   //添加流量方取消
        this.index.index=true;
        this.addflow.index=false;
      },
      fladdbtn(){               //确认添加流量方
        if(this.addflow.flname==''){
          this.$message({
            message:'请输入流量方名称',
            type:'warning'
          });
          this.$refs.flnames.focus();
          return;
        }else{
          var _this=this;
          this.$http.post('/Admin/InavAd/AddFlow',{
            Name:_this.addflow.flname,
          }).then(function(response){
            if(response.data.status=='ok'){
              _this.$message({
                message:'添加成功，即将跳转到首页',
                type:'success'
              });
              setTimeout(function(){
                _this.addflow.index=false;
                _this.index.index=true;
              },1400);
              _this.$http.post('/Admin/InavAd/GetFlowList').then(function(response){
                if(response.data.length>0){
                  _this.index.selects=response.data;
                  _this.index.select=_this.index.selects[0].Name;
                  for (let index = 0; index < _this.index.selects.length; index++) {
                    if(_this.index.select==_this.index.selects[index].Name){
                      _this.index.flowy=_this.index.selects[index].LastPv;
                      _this.index.flowt=_this.index.selects[index].CurPv;
                      _this.index.flowurl=_this.index.selects[index].Link;
                    }
                  }
                }else{
                  _this.index.placeh='暂无流量方';
                  _this.index.flowurl='请选择流量方';
                };
                
              }).catch(function(error){
                console.log(error);
                _this.$message({
                  message:'服务器错误',
                  type:'error'
                });
              });
            }else{
              _this.$message({
                message:'添加失败',
                type:'error'
              });
              return;
            }
            console.log(response);
          }).catch(function(error){
            console.log(error);
            _this.$message({
              message:'服务器错误',
              type:'error'
            });
          });
        }
      },
      setupback(){                //流量方设置返回
        this.index.index=true;
        this.flowsetup.index=false;
        this.inavsetup.index=false;
        this.index.pagin=true;
        this.inavsetup.pagin=false;
        //this.flowsetup.selectitem=[];
      },
      selectchange(type_id,items){               //下拉框改变
        var _this=this;
        _this.flowsetup.selectitem[items].jsonlist=[];
        if(_this.flowsetup.jsonlistinfo.length>0){
          for (let index = 0; index < _this.flowsetup.jsonlistinfo.length; index++) {
            if(_this.flowsetup.selectitem[items].list1===_this.flowsetup.jsonlistinfo[index].type_id){
              _this.flowsetup.selectitem[items].jsonlist.push(_this.flowsetup.jsonlistinfo[index]);
            }
          }
          if(_this.flowsetup.selectitem[items].jsonlist.length>0){
            _this.flowsetup.selectitem[items].list2=_this.flowsetup.selectitem[items].jsonlist[0].name;
          }else{
            _this.flowsetup.selectitem[items].list2='暂无品类';
          }
        }else{
          _this.flowsetup.list2='暂无品类'
        }
        
      },
      addselect(){                  //增加品类           
        this.flowsetup.selectitem.push({
          classlist:this.flowsetup.classlist,jsonlist:this.flowsetup.jsonlist,list1:this.flowsetup.classlist[0].type_id,list2:this.flowsetup.jsonlist[0].id
        });
      },
      deletesecelt(index){                      //删除品类
        this.flowsetup.selectitem.splice(index,1);
      },
      setupcommit(){                       //流量方设置提交
        var _this=this;
        _this.flowsetup.result=[];
        var items=_this.flowsetup.selectitem;
        for (let index = 0; index < items.length; index++) {
          switch (items[index].list2) {
            case '电脑及电脑周边':
              items[index].list2=1;
              break;
            case '医疗机构':
              items[index].list2=12;
              break;
            case '代理':
              items[index].list2=21;
              break;
            case '婚恋交友':
              items[index].list2=34;
              break;
            case '家装主材':
              items[index].list2=37;
              break;
            case '彩票':
              items[index].list2=44;
              break;
            case '房屋中介':
              items[index].list2=45;
              break;
            case '保养护肤':
              items[index].list2=52;
              break;
            case '美容减肥':
              items[index].list2=67;
              break;
            case '学前教育':
              items[index].list2=74;
              break;
            case '爱好收藏':
              items[index].list2=85;
              break;
            case '酒店住宿':
              items[index].list2=96;
              break;
            case '收纳整理':
              items[index].list2=105;
              break;
            case '女装':
              items[index].list2=110;
              break;
            case '家政':
              items[index].list2=124;
              break;
            case '面包蛋糕':
              items[index].list2=135;
              break;
            case '育儿网站':
              items[index].list2=145;
              break;
            case '车辆交易':
              items[index].list2=154;
              break;
            case '司法鉴定':
              items[index].list2=160;
              break;
            case '电商平台':
              items[index].list2=164;
              break;
            case '股票':
              items[index].list2=174;
              break;
            case '广告代理':
              items[index].list2=185;
              break;
            case '公益':
              items[index].list2=190;
              break;
            case '运动鞋':
              items[index].list2=191;
              break;
            case '休闲时间':
              items[index].list2=198;
              break;
            case '客户端游戏':
              items[index].list2=210;
              break;
            case '影音图像':
              items[index].list2=219;
              break;
            case '珠宝钻石':
              items[index].list2=234;
              break;
            default:
              break;
          }
          _this.flowsetup.result.push(items[index].list2);
        }
        var onlyArr=Array.from(new Set(_this.flowsetup.result));
        var resl=onlyArr.join(',');
        var flowid='';
        if(_this.index.select==_this.index.selects[0].FlowId){
          flowid=_this.index.selects[0].FlowId;
        }else{
          flowid=_this.index.select;
        }
        if(this.flowsetup.selectitem.length>0){
          this.$http.post('Admin/InavAd/ModifyFlowShieldList',{
            flowId:flowid,
            strList:resl
          }).then(function(response){
            if(response.data.status=='ok'){
              _this.$message({
                message:response.data.info,
                type:'success'
              });
              setTimeout(function() {
                _this.flowsetup.index=false;
                _this.index.index=true;
              }, 1000);
            }else{
              _this.$message({
                message:response.data.info,
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
        }else{
          this.$message({
            message:'请先添加广告品类',
            type:'warning'
          });
        }
      },
      addfilesuccess(response,file,fileList) {         //添加广告上传
        if(response.error!=0){
          this.$message({
            message:response.message,
            type:'warning'
          });
          this.addinav.inavAdResourceUrl='';
        }else{
          this.$message({
            message:'上传成功',
            type:'success'
          });
          this.addinav.inavAdResourceUrl=response.url;
          this.addinav.material=file.name;
        }
      },
      updatefilesuccess(response,file, fileList) {          //更新广告上传
        if(response.error!=0){
          this.$message({
            message:response.message,
            type:'warning'
          });
          this.inavupdate.inavAdResourceUrl='';
        }else{
          this.$message({
            message:'上传成功',
            type:'success'
          });
          this.inavupdate.inavAdResourceUrl=response.url;
          this.inavupdate.material=file.name;
        }
      },
      handleSizeChange(val) {    //分页功能
        this.index.pagesize=val;
        var _this=this;
        _this.index.currentPage=1;
        var currentpage=_this.index.currentPage;
        var pagesize=_this.index.pagesize;
        var flowid=_this.index.select;
        _this.getinavAdmgLists(currentpage,pagesize,flowid);
      },
      handleCurrentChange(val) {    //当前页改变
        var _this=this;
        _this.index.currentPage=val;
        var currentpage=_this.index.currentPage;
        var pagesize=_this.index.pagesize;
        var flowid=_this.index.select;
        _this.getinavAdmgLists(currentpage,pagesize,flowid);
      },
      handleSizeChanges(val) {    //广告设置分页功能
        var _this=this;
        _this.inavsetup.pagesize=val;
        _this.inavsetup.currentPage=1;
        var currentpage=_this.inavsetup.currentPage;
        var pagesize=_this.inavsetup.pagesize;
        _this.GetInavAdmglists(currentpage,pagesize);
      },
      handleCurrentChanges(val) {    //广告设置当前页改变
        var _this=this;
        _this.inavsetup.currentPage=val;
        var currentpage=_this.inavsetup.currentPage;
        var pagesize=_this.inavsetup.pagesize;
        _this.GetInavAdmglists(currentpage,pagesize);
      },
      detailSizeChange(val){           //详情每页个数
        var _this=this;
        _this.details.pagesize=val;
        _this.details.currentPage=1;
        var currentpage=_this.details.currentPage;
        var pagesize=_this.details.pagesize;
        var inavid=_this.details.inavadid;
        _this.GetdetailInfo(currentpage,pagesize,inavid,'','');
      },
      detailCurrentChange(val) {    //详情当前页改变
        var _this=this;
        _this.details.currentPage=val;
        var currentpage=_this.details.currentPage;
        var pagesize=_this.details.pagesize;
        var inavid=_this.details.inavadid;
        _this.GetdetailInfo(currentpage,pagesize,inavid,'','');
      },
    },
    created(){
      var _this=this;
      _this.GetDailySum();
      this.$http.post('/Admin/InavAd/GetFlowList').then(function(response){
        if(response.data.length>0){
          _this.index.selects=response.data;
          _this.index.select=_this.index.selects[0].FlowId;
          for (let index = 0; index < _this.index.selects.length; index++) {
            if(_this.index.select==_this.index.selects[index].FlowId){
              _this.index.flowy=_this.index.selects[index].LastPv;
              _this.index.flowt=_this.index.selects[index].CurPv;
              _this.index.flowurl=_this.index.selects[index].Link;
            }
          }
          _this.getinavAdmgLists(_this.index.currentPage,_this.index.pagesize,_this.index.select);
        }else{
          _this.index.placeh='暂无流量方';
          _this.index.flowurl='请选择流量方';
          _this.index.copybtn=false;
          _this.index.underbtn=false;
          _this.index.tabledata=[];
          _this.index.total=0;
        };
        
      }).catch(function(error){
        console.log(error);
        _this.$message({
          message:'服务器错误',
          type:'error'
        });
      });

      _this.GetAdClassList();   //初始化广告品类
      _this.GetTodayInfo();       //获取点击和曝光
    },
  }
</script>

<style scoped>

</style>

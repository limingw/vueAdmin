<template>
  <div class="finance">
    <!--查看图片遮罩-->
    <transition name="fade">
      <div class="wrap_img transition-box" @click="bgclick" v-show="index.bgif">
        <img :src="index.bgsrc" class="bigimg2">
      </div>
    </transition>

    <!--首页的四个按钮-->
    <div class="fisrtbtn" v-show="index.firstbtn">
      <el-row :gutter="0">
        <el-col :span="3" class="btnfir">
          <el-button type="primary" :class="[this.index.onclickc?'onclick':'point']" @click="financials()">财务数据</el-button>
        </el-col>
        <el-col :span="3" class="btnfir" :offset="1">
          <el-button type="primary" :class="[this.index.onclickk?'onclick':'point']" @click="apply()">开票申请</el-button>
        </el-col>
        <el-col :span="3" class="btnfir" :offset="1">
          <el-button type="primary" :class="[this.index.onclickd?'onclick':'point']" @click="pendings()">待授权</el-button>
        </el-col>
        <el-col :span="3" class="btnfir" :offset="1">
          <el-button type="primary" :class="[this.index.onclickw?'onclick':'point']" @click="notpass()">未通过</el-button>
        </el-col>
      </el-row>
    </div>
    
    <!--财务数据页面-->
      <div v-show="financial.index">
        <div class="info">
          <el-row class="rowbasic">
            <el-col :span="4">
              <span>基本数据</span>
            </el-col>
          </el-row>

          <el-row class="rowcome">
            <el-col :span="4" :offset="1">
              <span>当月收入：￥{{financial.nincome}}</span>
            </el-col>

            <el-col :span="4" :offset="0">
              <span>当月收益：￥{{financial.nprofit}}</span>
            </el-col>

            <el-col :span="5" :offset="2">
              <span>总收入：￥{{financial.aincome}}</span>
            </el-col>

            <el-col :span="4" :offset="0">
              <span>总收益：￥{{financial.aprofit}}</span>
            </el-col>

            <el-col :span="2" :offset="1" class="detailbtn">
              <el-button type="primary">详情</el-button>
            </el-col>
          </el-row>
        </div>

        <div class="secbtn">
          <el-row :gutter="0">
            <el-col :span="2" class="btnfir">
              <el-button type="primary" :class="[this.index.onclickkh?'onclick':'point']" @click="customs()">客户账户</el-button>
            </el-col>
            <el-col :span="2" class="btnfir" :offset="1">
              <el-button type="primary" :class="[this.index.onclickl?'onclick':'point']" @click="flows()">流量方账户</el-button>
            </el-col>
            <el-col :span="2" class="btnfir" :offset="1">
              <el-button type="primary" :class="[this.index.onclickagent?'onclick':'point']" @click="agents()">代理商账户</el-button>
            </el-col>
            <el-col :span="2" class="btnfir" :offset="1">
              <el-button type="primary" :class="[this.index.onclickch?'onclick':'point']" @click="pays()">充值记录</el-button>
            </el-col>
            <el-col :span="2" class="btnfir" :offset="1">
              <el-button type="primary" :class="[this.index.onclickz?'onclick':'point']" @click="transfers()">转账记录</el-button>
            </el-col>
            <el-col :span="2" class="btnfir" :offset="1">
              <el-button type="primary" :class="[this.index.onclickt?'onclick':'point']" @click="refunds()">退款记录</el-button>
            </el-col>
          </el-row>
        </div>

          <div class="khtable" v-show="index.khvshow">       <!--客户账户表格部分-->
            <el-row>
              <el-table border :data="index.khdata"
                :default-sort = "{prop: 'AccountBalance', order: 'descending'}">
                <el-table-column
                  header-align="center"
                  align="center"
                  prop="CompanyName"
                  label="公司名称"
                  sortable>
                </el-table-column>
                <el-table-column 
                  header-align="center"
                  align="center"
                  prop="AccountBalance"
                  label="账户余额"
                  sortable>
                </el-table-column>
                <el-table-column
                  header-align="center"
                  align="center"
                  label="操作"
                  width="200px">
                  <template slot-scope="scope">
                    <el-button type="primary" @click="recharge(scope.row,'0')" size="small">充值</el-button>
                    <el-button type="warning" @click="refund(scope.row,'1')" size="small">退款</el-button>
                  </template>
                </el-table-column>
              </el-table>
            </el-row>
          </div>

          <div class="llftable" v-show="index.llfvshow">       <!--流量方账户表格部分-->
            <el-row>
              <el-table border :data="index.llftable"
                :default-sort = "{prop: 'CompanyName', order: 'descending'}">
                <el-table-column
                  header-align="center"
                  align="center"
                  prop="CompanyName"
                  label="公司名称"
                  sortable>
                </el-table-column>
                <el-table-column 
                  header-align="center"
                  align="center"
                  prop="CompanyName"
                  label="未结算金额"
                  sortable>
                </el-table-column>
                <el-table-column
                  header-align="center"
                  align="center"
                  label="已结算金额"
                  :formatter="flowsmoney"
                  sortable>
                </el-table-column>
                <el-table-column 
                  header-align="center"
                  align="center"
                  prop="TotalInCome"
                  label="总收入"
                  sortable>
                </el-table-column>
                <el-table-column
                  header-align="center"
                  align="center"
                  label="操作"
                  width="200px">
                  <template slot-scope="scope">
                    <el-button type="primary" @click="recharge(scope.row,'0')" size="small">充值</el-button>
                    <el-button type="warning" @click="refund(scope.row,'1')" size="small">退款</el-button>
                  </template>
                </el-table-column>
              </el-table>
            </el-row>
          </div>

          <div class="agenttable" v-show="index.agentvshow">       <!--代理商账户表格部分-->
            <el-row>
              <el-table border :data="index.agentable"
                :default-sort = "{prop: 'RechargeCount', order: 'descending'}">
                <el-table-column
                  header-align="center"
                  align="center"
                  prop="AgentName"
                  label="代理商名称"
                  sortable>
                </el-table-column>
                <el-table-column 
                  header-align="center"
                  align="center"
                  prop="RechargeCount"
                  label="账户余额"
                  sortable>
                </el-table-column>
                <el-table-column
                  header-align="center"
                  align="center"
                  label="操作"
                  width="200px">
                  <template slot-scope="scope">
                    <el-button type="primary" @click="recharge(scope.row,'3')" size="small">充值</el-button>
                    <el-button type="warning" @click="refund(scope.row,'4')" size="small">退款</el-button>
                  </template>
                </el-table-column>
              </el-table>
            </el-row>
          </div>
        
          <div class="cztable" v-show="index.czvshow">       <!--充值记录表格部分-->
            <el-row>
              <el-table border :data="index.cztable"
                :default-sort = "{prop: 'ApplyDate', order: 'descending'}">
                <el-table-column
                  header-align="center"
                  align="center"
                  prop="ApplyDate"
                  label="充值时间"
                  :formatter="cztime"
                  width="160px"
                  sortable>
                </el-table-column>
                <el-table-column 
                  header-align="center"
                  align="center"
                  prop="CompanyName"
                  label="公司名称"
                  sortable>
                </el-table-column>
                <el-table-column
                  header-align="center"
                  align="center"
                  prop="RechargeSum"
                  label="充值金额"
                  sortable>
                </el-table-column>
                <el-table-column 
                  header-align="center"
                  align="center"
                  prop="ApplyPerson"
                  label="申请人">
                </el-table-column>
                <el-table-column
                  header-align="center"
                  align="center"
                  prop="AuthorPerson"
                  label="操作人">
                </el-table-column>
                <el-table-column
                  header-align="center"
                  align="center"
                  label="操作"
                  width="120px">
                  <template slot-scope="scope">
                    <el-button type="primary" @click="details(scope.row,'0')" size="small">详情</el-button>
                  </template>
                </el-table-column>
              </el-table>
            </el-row>
          </div>

          <div class="zztable" v-show="index.zzvshow">       <!--转账记录表格部分-->
            <el-row>
              <el-table border :data="index.zztable"
                :default-sort = "{prop: 'ApplayDate', order: 'descending'}">
                <el-table-column
                  header-align="center"
                  align="center"
                  prop="ApplayDate"
                  label="充值记录"
                  :formatter="cztime"
                  width="160px"
                  sortable>
                </el-table-column>
                <el-table-column 
                  header-align="center"
                  align="center"
                  prop="CompanyName"
                  label="公司名称">
                </el-table-column>
                <el-table-column
                  header-align="center"
                  align="center"
                  prop="TransferSum"
                  label="转账金额">
                </el-table-column>
                <el-table-column 
                  header-align="center"
                  align="center"
                  prop="AuthorPerson"
                  label="操作人">
                </el-table-column>
                <el-table-column
                  header-align="center"
                  align="center"
                  label="操作"
                  width="120px">
                  <template slot-scope="scope">
                    <el-button type="primary" @click="details(scope.row,'1')" size="small">详情</el-button>
                  </template>
                </el-table-column>
              </el-table>
            </el-row>
          </div>

          <div class="tktable" v-show="index.tkvshow">       <!--退款记录表格部分-->
            <el-row>
              <el-table border :data="index.tktable"
                :default-sort = "{prop: 'ApplayDate', order: 'descending'}">
                <el-table-column 
                  header-align="center"
                  align="center"
                  prop="ApplyDate"
                  label="退款时间"
                  :formatter="cztime"
                  width="160px"
                  sortable>
                </el-table-column>
                <el-table-column
                  header-align="center"
                  align="center"
                  prop="CompanyName"
                  label="公司名称">
                </el-table-column>
                <el-table-column 
                  header-align="center"
                  align="center"
                  prop="RefundsSum"
                  label="退款金额">
                </el-table-column>
                <el-table-column
                  header-align="center"
                  align="center"
                  prop="ApplyPerson"
                  label="申请人">
                </el-table-column>
                <el-table-column
                  header-align="center"
                  align="center"
                  prop="AuthorPerson"
                  label="操作人">
                </el-table-column>
                <el-table-column
                  header-align="center"
                  align="center"
                  label="操作"
                  width="120px">
                  <template slot-scope="scope">
                    <el-button type="primary" @click="details(scope.row,'2')" size="small">详情</el-button>
                  </template>
                </el-table-column>
              </el-table>
            </el-row>
          </div>
      </div>

      <!--开票申请-->
      <div class="serdbtn" v-show="applys.index">
        <el-row :gutter="0" class="elbtn">
          <el-col :span="2" class="btnfir">
            <el-button type="primary" :class="[this.applys.onclickldai?'onclick':'point']" @click="aready()">待开发票</el-button>
          </el-col>
          <el-col :span="2" class="btnfir" :offset="1">
            <el-button type="primary" :class="[this.applys.onclickredy?'onclick':'point']" @click="ready()">已开发票</el-button>
          </el-col>
        </el-row>

        <el-row>
          <el-table border :data="applys.tabledata"
            :default-sort = "{prop: 'ApplyDate', order: 'descending'}">
            <el-table-column
              header-align="center"
              align="center"
              prop="ApplyDate"
              label="申请日期"
              :formatter="cztime"
              width="160px"
              sortable>
            </el-table-column>
            <el-table-column 
              header-align="center"
              align="center"
              prop="CompanyName"
              label="公司名称">
            </el-table-column>
            <el-table-column
              header-align="center"
              align="center"
              prop="InvoiceAmount"
              label="开票金额">
            </el-table-column>
            <el-table-column 
              header-align="center"
              align="center"
              prop="RecordTime"
              label="开票时间"
              :formatter="applytime"
              sortable>
            </el-table-column>
            <el-table-column
              header-align="center"
              align="center"
              label="操作"
              width="120px">
              <template slot-scope="scope">
                <el-button type="primary" @click="details(scope.row,'3')" size="small">详情</el-button>
              </template>
            </el-table-column>
          </el-table>
        </el-row>
      </div>

    <!--待授权-->
      <div class="serdbtn" v-show="wait.index">
        <el-row>
          <el-table border :data="wait.tabledata"
            :default-sort = "{prop: 'ApplyDate', order: 'descending'}">
            <el-table-column
              header-align="center"
              align="center"
              prop="ApplyDate"
              label="申请时间"
              width="160px"
              :formatter="cztime"
              sortable>
            </el-table-column>
            <el-table-column 
              header-align="center"
              align="center"
              prop="CompanyName"
              label="申请公司">
            </el-table-column>
            <el-table-column
              header-align="center"
              align="center"
              prop="ApplyPerson"
              label="财务申请人">
            </el-table-column>
            <el-table-column 
              header-align="center"
              align="center"
              prop="Type"
              label="类型"
              :formatter="waitype">
            </el-table-column>
            <el-table-column
              header-align="center"
              align="center"
              prop="Money"
              label="金额"
              sortable>
            </el-table-column>
            <el-table-column 
              header-align="center"
              align="center"
              prop="ReservedField"
              :formatter="waitremark"
              label="备注">
            </el-table-column>
            <el-table-column
              header-align="center"
              align="center"
              label="操作"
              width="200px">
              <template slot-scope="scope">
                <el-button type="primary" @click="deal(scope.row)" size="small">处理</el-button>
                <el-button type="info" @click="close(scope.row)" size="small">关闭</el-button>
              </template>
            </el-table-column>
          </el-table>
        </el-row>
      </div>

    <!--未通过-->
      <div class="serdbtn" v-show="nopass.index">
        <el-row>
          <el-table border :data="nopass.tabledata"
            :default-sort = "{prop: 'ApplyDate', order: 'descending'}">
            <el-table-column
              header-align="center"
              align="center"
              prop="ApplyDate"
              label="申请时间"
              width="160px"
              :formatter="cztime"
              sortable>
            </el-table-column>
            <el-table-column 
              header-align="center"
              align="center"
              prop="ApplyPerson"
              label="申请人">
            </el-table-column>
            <el-table-column
              header-align="center"
              align="center"
              prop="Type"
              label="类型"
              :formatter="waitype">
            </el-table-column>
            <el-table-column 
              header-align="center"
              align="center"
              prop="AuthorPerson"
              label="授权人">
            </el-table-column>
            <el-table-column
              header-align="center"
              align="center"
              prop="Money"
              label="金额">
            </el-table-column>
            <el-table-column 
              header-align="center"
              align="center"
              prop="Detail"
              :formatter="noremark"
              label="备注">
            </el-table-column>
            <el-table-column
              header-align="center"
              align="center"
              prop="CheckDate"
              label="审核时间"
              :formatter="nopasstime"
              width="160px"
              sortable>
            </el-table-column>
            <el-table-column
              header-align="center"
              align="center"
              label="流水单">
              <template slot-scope="scope">
                <el-button type="primary" @click="lookwoter(scope.row)" size="small">查看</el-button>
              </template>
            </el-table-column>
          </el-table>
        </el-row>
      </div>

    <div class="pagin" v-show="applys.pagin">
      <el-row>
        <el-pagination class="pagin"
          @size-change="handleSizeChanges"
          @current-change="handleCurrentChanges"
          :current-page.sync="index.currentPage"
          :page-sizes="[2, 4, 6, 8]"
          :page-size="index.pagesize"
          :pager-count="5"
          layout="total, sizes, prev, pager, next, jumper"
          :total="index.total">
        </el-pagination>
      </el-row>
    </div>

    <!--分页-->
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
    
    <!--客户充值模态框-->
    <div class="diakhcz">
      <el-dialog
        :title="diakhzc.title"
        :visible.sync="diakhzc.diakhzc"
        >
        <el-row class="rowname rowbottom">
          <el-col :span="4" class="elmoney">
            <span>{{diakhzc.namespan}}</span>
          </el-col>

          <el-col :span="8">{{diakhzc.compnyname}}</el-col>
        </el-row>

        <el-row class="rowtype rowbottom">
          <el-col :span="4" class="elmoney">
            <span>开&nbsp;户&nbsp;类&nbsp;型：</span>
          </el-col>

          <el-col :span="8">{{diakhzc.opentype}}</el-col>
        </el-row>

        <el-row class="rowmoney rowbottom">
          <el-col :span="4" class="elmoney">
            <span>充&nbsp;值&nbsp;金&nbsp;额：</span>
          </el-col>

          <el-col :span="8">
            <el-input v-model="diakhzc.czmoney" ref="money" maxlength="15" placeholder="输入充值金额" clearable></el-input>
          </el-col>
        </el-row>

        <el-row class="rowbill rowbottom">
          <el-col :span="4" class="elmoney">
            <span>流&nbsp;&nbsp;&nbsp;水&nbsp;&nbsp;&nbsp;&nbsp;单：</span>
          </el-col>

          <el-col :span="5">
            <img src="../../src/assets/images/loginbg.png" v-if="diakhzc.khczvshow"/>
            <img :src="diakhzc.khczpic" @click="khpictr('0')" v-else />
          </el-col>

          <el-col :span="2" :offset="0">
            <el-upload
              class="upload-demo"
              :action="diakhzc.imgaction"
              :on-success="khczsuccess"
              :show-file-list="false"
              name="imgFile"
              list-type="picture">
              <el-button type="primary">上传</el-button>
            </el-upload>
          </el-col>
        </el-row>

        <el-row class="rowremark">
          <el-col :span="4" class="elmoney">
            <span>备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注：</span>
          </el-col>

          <el-col :span="8">
            <el-input
              type="textarea"
              :autosize="{ minRows: 5}"
              placeholder="请输入备注"
              v-model="diakhzc.txtremark">
            </el-input>
          </el-col>
        </el-row>
        <span slot="footer" class="dialog-footer">
          <el-button @click="khczreback()">取 消</el-button>
          <el-button type="primary" @click="diakhczsure()">确 定</el-button>
        </span>
      </el-dialog>
    </div>

    <!--开票申请模态框-->
    <div class="diapply">
      <el-dialog
        :title="diapply.title"
        :visible.sync="diapply.index"
        >
        <el-row class="rowbill rowbottom">
          <el-col :span="6" class="elmoney">
            <span>发&nbsp;&nbsp;票&nbsp;&nbsp;类&nbsp;&nbsp;型：</span>
          </el-col>

          <el-col :span="8">
            <el-select v-model="diapply.bill" placeholder="请选择发票类型">
              <el-option 
                v-for="item in diapply.invoicelist"
                :key="item.id"
                :label="item.name"
                :value="item.id">

              </el-option>
            </el-select>
          </el-col>
        </el-row>

        <el-row class="rowinavoicename rowbottom">
          <el-col :span="6" class="elmoney">
            <span>发&nbsp;&nbsp;票&nbsp;&nbsp;抬&nbsp;&nbsp;头：</span>
          </el-col>

          <el-col :span="8">
            <el-input v-model="diapply.inavoicename" ref="inavoicename" placeholder="发票抬头" maxlength="50" clearable></el-input>
          </el-col>
        </el-row>

        <el-row class="rowtaxpayernumber rowbottom">
          <el-col :span="6" class="elmoney">
            <span>纳&nbsp;税&nbsp;识&nbsp;别&nbsp;号：</span>
          </el-col>

          <el-col :span="8">
            <el-input v-model="diapply.taxpayernumber" ref="taxpayernumber" maxlength="50" placeholder="纳税识别号" clearable></el-input>
          </el-col>
        </el-row>

        <el-row class="rowcompanyaddr rowbottom">
          <el-col :span="6" class="elmoney">
            <span>企业注册地址：</span>
          </el-col>

          <el-col :span="8">
            <el-input v-model="diapply.companyaddr" ref="companyaddr" maxlength="50" placeholder="企业注册地址" clearable></el-input>
          </el-col>
        </el-row>

        <el-row class="rowcompanyphone rowbottom">
          <el-col :span="6" class="elmoney">
            <span>企&nbsp;&nbsp;业&nbsp;&nbsp;电&nbsp;&nbsp;话：</span>
          </el-col>

          <el-col :span="8">
            <el-input v-model="diapply.companyphone" ref="companyphone" maxlength="50" placeholder="企业电话" clearable></el-input>
          </el-col>
        </el-row>

        <el-row class="rowcompanyback rowbottom">
          <el-col :span="6" class="elmoney">
            <span>企业开户银行：</span>
          </el-col>

          <el-col :span="8">
            <el-input v-model="diapply.companyback" ref="companyback" maxlength="50" placeholder="企业开户银行" clearable></el-input>
          </el-col>
        </el-row>

        <el-row class="rowcompanycardnum rowbottom">
          <el-col :span="6" class="elmoney">
            <span>企业开户银行账号：</span>
          </el-col>

          <el-col :span="8">
            <el-input v-model="diapply.companycardnum" ref="companycardnum" maxlength="50" placeholder="企业开户银行账号" clearable></el-input>
          </el-col>
        </el-row>

        <el-row class="rowname rowbottom">
          <el-col :span="6" class="elmoney">
            <span>收&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;件&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;人：</span>
          </el-col>

          <el-col :span="8">
            <el-input v-model="diapply.name" ref="name" maxlength="50" placeholder="收件人" clearable></el-input>
          </el-col>
        </el-row>

        <el-row class="rowtelnumber rowbottom">
          <el-col :span="6" class="elmoney">
            <span>收&nbsp;件&nbsp;人&nbsp;电&nbsp;话：</span>
          </el-col>

          <el-col :span="8">
            <el-input v-model="diapply.telnumber" ref="telnumber" maxlength="50" placeholder="收件人电话" clearable></el-input>
          </el-col>
        </el-row>

        <el-row class="rowemail rowbottom">
          <el-col :span="6" class="elmoney">
            <span>收&nbsp;件&nbsp;人&nbsp;邮&nbsp;箱：</span>
          </el-col>

          <el-col :span="8">
            <el-input v-model="diapply.email" ref="email" maxlength="50" placeholder="收件人邮箱" clearable></el-input>
          </el-col>
        </el-row>

        <el-row class="rowaddress rowbottom">
          <el-col :span="6" class="elmoney">
            <span>配&nbsp;&nbsp;送&nbsp;&nbsp;地&nbsp;&nbsp;址：</span>
          </el-col>

          <el-col :span="8">
            <el-input v-model="diapply.address" ref="address" maxlength="50" placeholder="配送地址" clearable></el-input>
          </el-col>
        </el-row>

        <el-row class="rowexpress rowbottom">
          <el-col :span="6" class="elmoney">
            <span>快&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;递：</span>
          </el-col>

          <el-col :span="8">
            <el-select v-model="diapply.express" placeholder="请选择快递">
              <el-option 
                v-for="item in diapply.expresslist"
                :key="item.id"
                :label="item.name"
                :value="item.id">

              </el-option>
            </el-select>
          </el-col>
        </el-row>

        <el-row class="rowcouriernumber rowbottom">
          <el-col :span="6" class="elmoney">
            <span>快&nbsp;&nbsp;递&nbsp;&nbsp;单&nbsp;&nbsp;号：</span>
          </el-col>

          <el-col :span="8">
            <el-input v-model="diapply.couriernumber" ref="couriernumber" maxlength="50" placeholder="快递单号" clearable></el-input>
          </el-col>
        </el-row>
        <span slot="footer" class="dialog-footer">
          <el-button @click="diapply.index=false">取 消</el-button>
          <el-button type="primary" @click="diapplysure()">确 定</el-button>
        </span>
      </el-dialog>
    </div>

    <!--待授权模态框-->
    <div class="diakhcz">
      <el-dialog
        title="授权"
        :visible.sync="pending.index"
        >
        <el-row class="rowname rowbottom">
          <el-col :span="3" class="elmoney">
            <span>类&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;型：</span>
          </el-col>

          <el-col :span="8">{{pending.type}}</el-col>
        </el-row>

        <el-row class="rowtype rowbottom">
          <el-col :span="3" class="elmoney">
            <span>公司名称：</span>
          </el-col>

          <el-col :span="8">{{pending.companyname}}</el-col>
        </el-row>

        <el-row class="rowmoney rowbottom">
          <el-col :span="3" class="elmoney">
            <span>开&nbsp;&nbsp;户&nbsp;&nbsp;行：</span>
          </el-col>

          <el-col :span="8">
            <el-input v-model="pending.companybank" ref="trcompanybank" maxlength="15" placeholder="开户行" clearable></el-input>
          </el-col>
        </el-row>

        <el-row class="rowmoney rowbottom">
          <el-col :span="3" class="elmoney">
            <span>开户账号：</span>
          </el-col>

          <el-col :span="8">
            <el-input v-model="pending.companycardnum" ref="trcompanycardnum" maxlength="15" placeholder="开户账号" clearable></el-input>
          </el-col>
        </el-row>

        <el-row class="rowbill rowbottom">
          <el-col :span="3" class="elmoney">
            <span>流&nbsp;&nbsp;水&nbsp;&nbsp;单：</span>
          </el-col>

          <el-col :span="5">
            <img src="../../src/assets/images/loginbg.png" v-if="pending.imgvshow"/>
            <img :src="pending.grantsrc" v-else @click="grant()" />
          </el-col>

          <el-col :span="2" :offset="0">
            <el-button type="primary" @click="grant()">查看</el-button>
          </el-col>
        </el-row>

        <el-row class="rowremark">
          <el-col :span="3" class="elmoney">
            <span>备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注：</span>
          </el-col>

          <el-col :span="8">
            <el-input
              type="textarea"
              :autosize="{ minRows: 5}"
              placeholder="请输入备注"
              v-model="pending.txtremark">
            </el-input>
          </el-col>
        </el-row>
        <span slot="footer" class="dialog-footer">
          <el-button @click="grantreback()">取 消</el-button>
          <el-button type="primary" @click="grantsure()">确 定</el-button>
        </span>
      </el-dialog>
    </div>

    <!--退款模态框-->
    <div class="diakhcz">
      <el-dialog
        :title="diarefund.title"
        :visible.sync="diarefund.index"
        >
        <el-row class="rowtype rowbottom">
          <el-col :span="4" class="elmoney">
            <span>{{diarefund.namespan}}</span>
          </el-col>

          <el-col :span="8">{{diarefund.companyname}}</el-col>
        </el-row>

        <el-row class="rowname rowbottom">
          <el-col :span="4" class="elmoney">
            <span>开&nbsp;户&nbsp;类&nbsp;型：</span>
          </el-col>

          <el-col :span="8">{{diarefund.opentype}}</el-col>
        </el-row>

        <el-row class="rowmoney rowbottom">
          <el-col :span="4" class="elmoney">
            <span>开&nbsp;&nbsp;&nbsp;户&nbsp;&nbsp;&nbsp;行：</span>
          </el-col>

          <el-col :span="8">
            <el-input v-model="diarefund.companybank" ref="recompanybank" maxlength="15" placeholder="开户行" clearable></el-input>
          </el-col>
        </el-row>

        <el-row class="rowmoney rowbottom">
          <el-col :span="4" class="elmoney">
            <span>开&nbsp;户&nbsp;账&nbsp;号：</span>
          </el-col>

          <el-col :span="8">
            <el-input v-model="diarefund.companycardnum" ref="recompanycardnum" maxlength="15" placeholder="开户账号" clearable></el-input>
          </el-col>
        </el-row>

        <el-row class="rowmoney rowbottom">
          <el-col :span="4" class="elmoney">
            <span>退&nbsp;款&nbsp;金&nbsp;额：</span>
          </el-col>

          <el-col :span="8">
            <el-input v-model="diarefund.refundmoney" ref="recompanymoney" maxlength="15" placeholder="退款金额" clearable></el-input>
          </el-col>
        </el-row>

        <el-row class="rowbill rowbottom">
          <el-col :span="4" class="elmoney">
            <span>流&nbsp;&nbsp;&nbsp;水&nbsp;&nbsp;&nbsp;单：</span>
          </el-col>

          <el-col :span="5">
            <img src="../../src/assets/images/loginbg.png" v-if="diarefund.khczvshow"/>
            <img :src="diarefund.picurl" @click="khpictr('1')" v-else />
          </el-col>

          <el-col :span="2" :offset="0">
            <el-upload
              class="upload-demo"
              :action="diakhzc.imgaction"
              :on-success="refundsuccess"
              :show-file-list="false"
              name="imgFile"
              list-type="picture">
              <el-button type="primary">上传</el-button>
            </el-upload>
          </el-col>
        </el-row>

        <el-row class="rowremark">
          <el-col :span="4" class="elmoney">
            <span>备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注：</span>
          </el-col>

          <el-col :span="8">
            <el-input
              type="textarea"
              :autosize="{ minRows: 5}"
              placeholder="请输入备注"
              v-model="diarefund.txtremark">
            </el-input>
          </el-col>
        </el-row>
        <span slot="footer" class="dialog-footer">
          <el-button @click="diarefund.index=false">取 消</el-button>
          <el-button type="primary" @click="refundsure()">确 定</el-button>
        </span>
      </el-dialog>
    </div>

    <!--详情模态框-->
    <div class="diakhcz">
      <el-dialog
        :title="diadetail.title"
        :visible.sync="diadetail.index"
        >
        <el-row class="rowbill rowbottom">
          <el-col :span="3" class="elmoney">
            <span>流&nbsp;&nbsp;水&nbsp;&nbsp;单：</span>
          </el-col>

          <el-col :span="5">
            <img src="../../src/assets/images/loginbg.png" v-if="diadetail.imgvif" />
            <img :src="diadetail.imgurl" v-else @click="detailimg()" />
          </el-col>

          <el-col :span="2" :offset="0">
            <el-button type="primary" @click="detailimg()">查看</el-button>
          </el-col>
        </el-row>

        <el-row class="rowremark">
          <el-col :span="3" class="elmoney">
            <span>备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注：</span>
          </el-col>

          <el-col :span="8">
            <el-input
              type="textarea"
              :autosize="{ minRows: 5}"
              placeholder="充值记录备注"
              v-model="diadetail.remark">
            </el-input>
          </el-col>
        </el-row>
        <span slot="footer" class="dialog-footer">
          <el-button type="button" @click="diadetail.index=false">关 闭</el-button>
        </span>
      </el-dialog>
    </div>
  </div>
</template>

<script>
  import '../css/finance.less'
  import '../js/axios.js'
import index from '../store';
  export default {
    name: "Finance",
    data () {
      return {
        index:{
          firstbtn:true,           //按钮v-show
          onclickc:true,          //控制按钮样式
          onclickk:false,
          onclickd:false,
          onclickw:false,
          onclickkh:true,
          onclickl:false,
          onclickagent:false,
          onclickch:false,
          onclickz:false,
          onclickt:false,
          khdata:[],                   //客户账户table
          khvshow:true,
          llftable:[],                    //流量方账户表格
          llfvshow:false,
          agentable:[],                   //代理商账户
          agentvshow:false,
          cztable:[],                 //充值记录表格
          czvshow:false,
          zztable:[],                  //转账记录表格
          zzvshow:false,
          tktable:[],                   //退款记录表格
          tkvshow:false,
          currentPage:1,            //当前页
          pagesize:8,              //每页大小
          total:1,                    //总数
          pagin:true,                    //分页v-if
          bgif:false,                    //dialog点击图片遮罩
          bgsrc:'',                       
        },
        financial:{
          index:true,           //财务数据v-show
          nincome:0,              //当月收入
          nprofit:0,             //当月收益
          aincome:0,              //总收入
          aprofit:0,                //总收益
          uid:'',               //当前uid
        },
        applys:{               //开票申请
          index:false,
          onclickldai:true,        //待开发票
          onclickredy:false,         //已开发票
          tabledata:[],
          currentPage:1,            //当前页
          pagesize:8,              //每页大小
          total:1,                    //总数
          pagin:false,                    //分页v-if
        },
        wait:{                       //待授权
          index:false,
          tabledata:[],
        },
        nopass:{                    //未通过
          index:false,
          tabledata:[],
        },
        diakhzc:{                 //客户充值对话框
          diakhzc:false,             //充值对话框
          id:'',
          title:'客 户 充 值',
          imgaction:'/ToolFiles/kindeditor/asp.net/imguploadToOss.ashx?modulename=aptitude&uid=',                 //流水单上传地址
          namespan:'公司名称：',          //用于区分客户还是代理商充值   
          compnyname:'',            //公司名称
          opentype:'',                 //开户类型
          czmoney:'',                      //充值金额
          txtremark:'',                    //充值备注
          khczpic:'',       //客户充值流水单
          responseurl:'',
          khczvshow:true,
        },
        diapply:{                         //开票申请对话框
          index:false,
          id:'',
          title:'开票申请',
          invoicelist:[],                 //发票类型
          bill:'',
          inavoicename:'',                //发票抬头
          taxpayernumber:'',                //纳税识别号
          companyaddr:'',                 //企业注册地址
          companyphone:'',                //企业电话
          companyback:'',                 //企业银行
          companycardnum:'',               //企业开户行账号
          name:'',                        //收件人
          telnumber:'',                    //收件人电话
          email:'',                       //收件人邮箱
          address:'',                     //配送地址
          expresslist:[],                   //快递
          express:'',  
          couriernumber:'',               //快递单号
        },
        pending:{                      //待授权对话框
          index:false,
          imgvshow:true,
          id:'',
          orderid:'',
          type:'',                  //类型
          typevalue:'',
          companyname:'',                 //公司名称
          companybank:'',                //开户行
          companycardnum:'',               //开户账号
          grantsrc:'',                   //流水单
          txtremark:'',                   //备注
          ApplyDate:'',
          ApplyPerson:'',
          TransicationKey:'',
          Money:'',
        },
        diarefund:{                      //退款对话框
          index:false,
          id:'',
          title:'客户退款',
          namespan:'客 户 退 款',
          type:'',                  //类型
          companyname:'',                 //公司名称
          companybank:'',                //开户行
          companycardnum:'',               //开户账号
          opentype:'',                 //开户类型
          refundmoney:'',                 //退款金额
          picurl:'',       //流水单
          responseurl:'',
          khczvshow:true,
          txtremark:'',                   //备注
        },
        diadetail:{                   //充值记录详情
          index:false,
          imgvif:true,
          title:'充值记录详情',
          imgurl:'',
          remark:'',
        },
      }
    },
    methods:{
      paginhide(){    //分页隐藏
        var _this=this;
        if(_this.index.total<=0){
          _this.index.pagin=false;
        }else{
          _this.index.pagin=true;
        }
      },
      GetIndexInfo(){      //获取首页基本信息
        var _this=this;
        _this.$http.post('/Admin/Finance/VueGetIndex').then(function(response){
          var info=response.data;
          if(info!=''||info!=null){
            _this.financial.nincome=info.AccountingDataList.MonthIncome;       //首页信息赋值
            _this.financial.nprofit=info.AccountingDataList.MonthReturn;
            _this.financial.aincome=info.AccountingDataList.TotalIncome;
            _this.financial.aprofit=info.AccountingDataList.TotalReturn;
            _this.diapply.invoicelist=info.InvoiceTypeTbList;
            _this.diapply.expresslist=info.ExpressList;
            _this.diapply.bill=info.InvoiceTypeTbList[0].id;
            _this.diapply.express=info.ExpressList[0].id;
            _this.financial.uid=info.Uid;
            _this.diakhzc.imgaction='/ToolFiles/kindeditor/asp.net/imguploadToOss.ashx?modulename=aptitude&uid='+_this.financial.uid;
          }else{
            _this.financial.nincome=0;
            _this.financial.nprofit=0;
            _this.financial.aincome=0;
            _this.financial.aprofit=0;
            _this.diapply.invoicelist=[];
            _this.diapply.expresslist=[];
            _this.financial.uid='';
          }
        }).catch(function(error){
          console.log(error);
          _this.$message({
            message:'服务器错误',
            type:'error'
          });
        });
      },
      GetCustomInfo(pagesize,currentpage){           //获取客户账户余额信息
        var _this=this;
        _this.index.khdata=[];
        var params={
          pageSize:pagesize,
          pageNo:(currentpage-1)*pagesize
        }
        _this.$http.get('/Admin/Finance/VueGetCustomInfo',{
          params:params
        }).then(function(response){
          var info=response.data;
          if(info!=''||info!=null){
            _this.index.khdata=info.CustomDTOList;
            _this.index.total=info.TotalList;
          }else{
            _this.index.khdata=[];
            _this.index.total=0;
          }

          _this.paginhide();             //判断是否启用分页
        }).catch(function(error){
          console.log(error);
          _this.$message({
            message:'服务器错误',
            type:'error'
          });
        });
      },
      GetFlowsInfo(pagesize,currentpage){      //获取流量方账户信息
        var _this=this;
        _this.index.llftable=[];
        var params={
          pageSize:pagesize,
          pageNo:(currentpage-1)*pagesize
        }
        _this.$http.get('/Admin/Finance/VueGetFlowAccountRecordInfo',{
          params:params
        }).then(function(response){
          var info=response.data;
          if(info!=''||info!=null){
            _this.index.llftable=info.FlowDTOList;
            _this.index.total=info.TotalList;
          }else{
            _this.index.llftable=[];
            _this.index.total=0;
          }

          _this.paginhide();             //判断是否启用分页
        }).catch(function(error){
          console.log(error);
          _this.$message({
            message:'服务器错误',
            type:'error'
          });
        });
      },
      GetAgentInfo(pagesize,currentpage){           //获取代理商信息
        var _this=this;
        _this.index.agentable=[];
        var params={
          pageSize:pagesize,
          pageNo:(currentpage-1)*pagesize
        };
        _this.$http.get('/Admin/Finance/VueGetAgentFinanceRecordInfo',{
          params:params
        }).then(function(response){
          var info=response.data;
          if(info!=''||info!=null){
            _this.index.agentable=info.AgentFinanceMgDTOList;
            _this.index.total=info.TotalList;
          }else{
            _this.index.agentable=[];
            _this.index.total=0;
          }

          _this.paginhide();             //判断是否启用分页
        }).catch(function(error){
          console.log(error);
          _this.$message({
            message:'服务器错误',
            type:'error'
          });
        });
      },
      GetRechargeInfo(pagesize,currentpage){      //获取充值记录
        var _this=this;
        _this.index.cztable=[];
        var params={
          pageSize:pagesize,
          pageNo:(currentpage-1)*pagesize
        }
        _this.$http.get('/Admin/Finance/VueGetRechargeRecord',{
          params:params
        }).then(function(response){
          var info=response.data;
          if(info!=''||info!=null){
            _this.index.cztable=info.RechargeDTOList;
            _this.index.total=info.TotalList;
          }else{
            _this.index.cztable=[];
            _this.index.total=0;
          }

          _this.paginhide();             //判断是否启用分页
        }).catch(function(error){
          console.log(error);
          _this.$message({
            message:'服务器错误',
            type:'error'
          });
        });
      },
      GetTransferInfo(pagesize,currentpage){      //获取转账记录
        var _this=this;
        _this.index.zztable=[];
        var params={
          pageSize:pagesize,
          pageNo:(currentpage-1)*pagesize
        }
        _this.$http.get('/Admin/Finance/VueGetFlowTransferRecord',{
          params:params
        }).then(function(response){
          var info=response.data;
          if(info!=''||info!=null){
            _this.index.zztable=info.TransferDTOList;
            _this.index.total=info.TotalList;
          }else{
            _this.index.zztable=[];
            _this.index.total=0;
          }

          _this.paginhide();             //判断是否启用分页
        }).catch(function(error){
          console.log(error);
          _this.$message({
            message:'服务器错误',
            type:'error'
          });
        });
      },
      GetRefundsInfo(pagesize,currentpage){      //获取退款记录
        var _this=this;
        _this.index.tktable=[];
        var params={
          pageSize:pagesize,
          pageNo:(currentpage-1)*pagesize
        }
        _this.$http.get('/Admin/Finance/VueGetRefundsRecord',{
          params:params
        }).then(function(response){
          var info=response.data;
          if(info!=''||info!=null){
            _this.index.tktable=info.RefundsRecordDTOList;
            _this.index.total=info.TotalList;
          }else{
            _this.index.tktable=[];
            _this.index.total=0;
          }

          _this.paginhide();             //判断是否启用分页
        }).catch(function(error){
          console.log(error);
          _this.$message({
            message:'服务器错误',
            type:'error'
          });
        });
      },
      GetUnauthorizedInfo(pagesize,currentpage){      //获取待授权记录
        var _this=this;
        _this.wait.tabledata=[];
        var params={
          pageSize:pagesize,
          pageNo:(currentpage-1)*pagesize
        }
        _this.$http.get('/Admin/Finance/VueGetUnauthorizedRecord',{
          params:params
        }).then(function(response){
          var info=response.data;
          if(info!=''||info!=null){
            _this.wait.tabledata=info.UnauthorizedRecordTbDTOList;
            _this.index.total=info.TotalList;
          }else{
            _this.wait.tabledata=[];
            _this.index.total=0;
          }

          _this.paginhide();             //判断是否启用分页
        }).catch(function(error){
          console.log(error);
          _this.$message({
            message:'服务器错误',
            type:'error'
          });
        });
      },
      GetNotauthorizedInfo(pagesize,currentpage){      //获取未通过待授权记录
        var _this=this;
        _this.nopass.tabledata=[];
        var params={
          pageSize:pagesize,
          pageNo:(currentpage-1)*pagesize
        }
        _this.$http.get('/Admin/Finance/VueGetNotauthorizedRecord',{
          params:params
        }).then(function(response){
          var info=response.data;
          if(info!=''||info!=null){
            _this.nopass.tabledata=info.NotAuthorisedRecordTbDTOList;
            _this.index.total=info.TotalList;
          }else{
            _this.nopass.tabledata=[];
            _this.index.total=0;
          }

          _this.paginhide();             //判断是否启用分页
        }).catch(function(error){
          console.log(error);
          _this.$message({
            message:'服务器错误',
            type:'error'
          });
        });
      },
      GetInavoiceInfo(pagesize,currentpage,status){      //获取开票申请记录
        var _this=this;
        _this.applys.tabledata=[];
        var params={
          pageSize:pagesize,
          pageNo:(currentpage-1)*pagesize,
          invoiceStatus:status
        }
        _this.$http.get('/Admin/Finance/VueGetInvoiceRecord',{
          params:params
        }).then(function(response){
          var info=response.data;
          if(info!=''||info!=null){
            _this.applys.tabledata=info.InvoiceRecordDTO;
            _this.index.total=info.TotalList;
          }else{
            _this.applys.tabledata=[];
            _this.index.total=0;
          }

          _this.paginhide();             //判断是否启用分页
        }).catch(function(error){
          console.log(error);
          _this.$message({
            message:'服务器错误',
            type:'error'
          });
        });
      },
      Unauthorize(id,foreignid,imgkey,money,type,remark){          //充值退款转账接口
        var _this=this;
        var datainfo={
          id:id,
          ForeignId:foreignid,
          TransicationKey:imgkey,
          Money:parseFloat(money),
          Type:type,
          ReservedField:remark
        };

        if(type==0||type==1||type==2){                  //客户充值转账退款接口
          _this.$http.post('/Admin/Finance/AddUnauthorizedRecord',datainfo).then(function(response){
            var info=response.data;
            if(info.status=='yes'){
              _this.$message({
                message:'操作成功',
                type:"success",
              });
            }else{
              _this.$message({
                message:'操作失败',
                type:"error",
              });
            }
            _this.GetCustomInfo(_this.index.pagesize,_this.index.currentPage);
            _this.diakhzc.diakhzc=false;
            _this.diarefund.index=false;
          }).catch(function(error){
            console.log(error);
            _this.$message({
              message:'服务器错误',
              type:'error'
            });
          });
        }else if(type==3||type==4){                   //代理商充值退款
          _this.$http.post('/Admin/AgentFinance/AgentRecharge',datainfo).then(function(response){
            var info=response.data;
            if(info.status=='yes'){
              _this.$message({
                message:'操作成功',
                type:"success",
              });
            }else{
              _this.$message({
                message:'操作失败',
                type:"error",
              });
            }
            _this.GetAgentInfo(_this.index.pagesize,_this.index.currentPage);                //加上获取代理商信息方法
            _this.diakhzc.diakhzc=false;
            _this.diarefund.index=false;
          }).catch(function(error){
            console.log(error);
            _this.$message({
              message:'服务器错误',
              type:'error'
            });
          });
        }
        
      }, 
      GetImage(imgurl){                  //获取流水单
        var _this=this;
        _this.diadetail.imgurl='';
        _this.pending.grantsrc='';
        _this.$http.post('/Admin/Finance/GetImgUrl',{
          imgUrl:imgurl
        }).then(function(response){
          var info=response.data;
          if(info!=''||info!=null){
            _this.index.bgsrc=info.imgUrl;
            _this.diadetail.imgurl=info.imgUrl;
            _this.pending.grantsrc=info.imgUrl;
            if(_this.diadetail.imgurl!=''){
              _this.diadetail.imgvif=false;
            }else{
              _this.diadetail.imgvif=true;
            }

            if(_this.pending.grantsrc!=''){
              _this.pending.imgvshow=false;
            }else{
              _this.pending.imgvshow=true;
            }
          }else{
            _this.index.bgsrc='';
          }
        }).catch(function(error){
          console.log(error);
          _this.$message({
            message:'服务器错误',
            type:'error'
          });
        });
      },
      authorizationAgree(date,person,name,type,imgurl,foreignid,id,money,remark){         //待授权同意
        var _this=this;
        var datainfo={
          ApplayDate:date,
          ApplyPerson:person,
          CompanyName:name,
          Type:type,
          TransicationKey:imgurl,
          ForeignId:foreignid,
          id:id,
          Money:parseFloat(money),
          ReservedField:remark
        };

        if(type==0||type==1||type==2){                 //客户充值退款
          _this.$http.post('/Admin/Finance/AuthorizationAgree',datainfo).then(function(response){
            var info=response.data;
            if(info.status=='yes'){
              _this.$message({
                message:'操作成功',
                type:"success",
              });
            }else{
              _this.$message({
                message:'操作失败',
                type:"error",
              });
            }
            _this.GetUnauthorizedInfo(_this.index.pagesize,_this.index.currentPage);
            _this.pending.index=false;
          }).catch(function(error){
            console.log(error);
            _this.$message({
              message:'服务器错误',
              type:'error'
            });
          });
        }else if(type==3||type==4){                      //代理商充值退款
          _this.$http.post('/Admin/AgentFinance/AgentRecharge',datainfo).then(function(response){
            var info=response.data;
            if(info.status=='yes'){
              _this.$message({
                message:'操作成功',
                type:"success",
              });
            }else{
              _this.$message({
                message:'操作失败',
                type:"error",
              });
            }
            _this.GetUnauthorizedInfo(_this.index.pagesize,_this.index.currentPage);
            _this.pending.index=false;
          }).catch(function(error){
            console.log(error);
            _this.$message({
              message:'服务器错误',
              type:'error'
            });
          });
        }
      },
      authorizationUnAgree(date,person,name,type,imgurl,foreignid,id,money,remark){         //待授权同意
        var _this=this;
        var datainfo={
          ApplayDate:date,
          ApplyPerson:person,
          CompanyName:name,
          Type:type,
          TransicationKey:imgurl,
          ForeignId:foreignid,
          id:id,
          Money:parseFloat(money),
          ReservedField:remark
        };

        _this.$http.post('/Admin/Finance/authorizationUnAgree',datainfo).then(function(response){
            var info=response.data;
            if(info.status=='yes'){
              _this.$message({
                message:'操作成功',
                type:"success",
              });
            }else{
              _this.$message({
                message:'操作失败',
                type:"error",
              });
            }
            _this.GetUnauthorizedInfo(_this.index.pagesize,_this.index.currentPage);
          }).catch(function(error){
            console.log(error);
            _this.$message({
              message:'服务器错误',
              type:'error'
            });
          });
      },
      financials(){       //财务数据
        this.index.onclickc=true;
        this.index.onclickk=false;
        this.index.onclickd=false;
        this.index.onclickw=false;
        this.financial.index=true;
        this.applys.index=false;
        this.wait.index=false;
        this.nopass.index=false;
        this.applys.pagin=false;

        this.GetIndexInfo();       //财务数据信息接口
        var pagesize=this.index.pagesize;
        var currentpage=this.index.currentPage;
        this.GetCustomInfo(pagesize,currentpage);      //客户账户信息接口调用
      },
      apply(){            //开票申请
        this.index.onclickc=false;
        this.index.onclickk=true;
        this.index.onclickd=false;
        this.index.onclickw=false;
        this.financial.index=false;
        this.applys.index=true;
        this.wait.index=false;
        this.nopass.index=false;
        this.index.pagin=true;

        this.diakhzc.diakhzc=false;
        this.diakhzc.khczpic='';
        this.diakhzc.khczvshow=true;

        var pagesize=this.index.pagesize;
        var currentpage=this.index.currentPage;
        this.GetInavoiceInfo(pagesize,currentpage,'0');         //获取开票记录
      }, 
      applytime(row){                       //开票申请格式化时间
        if(row.RecordTime!=''||row.RecordTime!=null){
          var time=row.RecordTime.replace('T',' ');
          time=time.split('.')[0];
          row.RecordTime=time;
          return time;
        }
      },
      pendings(){           //待授权
        this.index.onclickc=false;
        this.index.onclickk=false;
        this.index.onclickd=true;
        this.index.onclickw=false;
        this.financial.index=false;
        this.applys.index=false;
        this.wait.index=true;
        this.nopass.index=false;
        this.index.pagin=true;

        var pagesize=this.index.pagesize;
        var currentpage=this.index.currentPage;
        this.GetUnauthorizedInfo(pagesize,currentpage);      //获取待授权
      },
      waitype(row){                         //待授权类型判断
        switch (row.Type) {
          case 0:
            return "充值"
            break;
          case 1:
            return "退款"
            break;
          case 2:
            return "转账"
            break;
          case 3:
            return "充值"
            break;
          case 4:
            return "退款"
            break;
          default:
            break;
        }
      },
      deal(row){                       //待授权处理按钮
        var _this=this;
        _this.pending.companybank='';
        _this.pending.companycardnum='';
        _this.pending.txtremark='';
        
        switch (row.Type) {
          case 0:
            _this.pending.type='充值';
            break;
          case 1:
            _this.pending.type='退款';
            break;
          case 2:
            _this.pending.type='转账';
            break;
          case 3:
            _this.pending.type='充值';
            break;
          case 4:
            _this.pending.type='退款';
            break;
          default:
            break;
        }
        _this.pending.companyname=row.CompanyName;        //模态框赋值
        _this.pending.id=row.ForeignId;
        _this.pending.orderid=row.id;
        _this.pending.typevalue=row.Type;
        _this.GetImage(row.TransicationKey);
        _this.pending.TransicationKey=row.TransicationKey;
        _this.pending.ApplyDate=row.ApplayDate;
        _this.pending.ApplyPerson=row.ApplyPerson;
        _this.pending.Money=row.Money;
        _this.pending.grantsrc=_this.index.bgsrc;
        _this.pending.index=true;
      },
      close(row){                       //待授权关闭按钮
        var _this=this;    
        _this.pending.companyname=row.CompanyName;        //模态框赋值
        _this.pending.id=row.ForeignId;
        _this.pending.orderid=row.id;
        _this.pending.typevalue=row.Type;
        _this.GetImage(row.TransicationKey);
        _this.pending.TransicationKey=row.TransicationKey;
        _this.pending.ApplyDate=row.ApplayDate;
        _this.pending.ApplyPerson=row.ApplyPerson;
        _this.pending.Money=row.Money;
        _this.pending.grantsrc=_this.index.bgsrc;
        var type=0;
        switch (row.Type) {
          case '0':
            type=0;
            break;
          case '1':
            type=1;
            break;
          case '2':
            type=2;
            break;
          case '3':
            type=0;
            break;
          case '4':
            type=1;
            break;
          default:
            break;
        }
        _this.authorizationUnAgree(_this.pending.ApplyDate,_this.pending.ApplyPerson,_this.pending.companyname,type,_this.pending.TransicationKey,_this.pending.id,_this.pending.orderid,_this.pending.Money,'');
      },
      waitremark(row){                      //待授权备注格式化
        if(row.ReservedField==''||row.ReservedField==null){
          return '/';
        }else{
          return row.ReservedField;
        }
      },
      notpass(){            //未通过
        this.index.onclickc=false;
        this.index.onclickk=false;
        this.index.onclickd=false;
        this.index.onclickw=true;
        this.financial.index=false;
        this.applys.index=false;
        this.wait.index=false;
        this.nopass.index=true;
        this.index.pagin=true;

        var pagesize=this.index.pagesize;
        var currentpage=this.index.currentPage;
        this.GetNotauthorizedInfo(pagesize,currentpage);          //获取未通过
      },
      nopasstime(row){                         //未通过审核时间格式化
        if(row.CheckDate!=''||row.CheckDate!=null){
          var time=row.CheckDate.replace('T',' ');
          time=time.split('.')[0];
          row.CheckDatel=time;
          return time;
        }
      },
      lookwoter(row){                       //未通过查看流水单
        var _this=this;
        _this.index.bgsrc='';
        _this.index.bgif=true;
        if(row.TransicationKey!=''||row.TransicationKey!=null){
          _this.GetImage(row.TransicationKey);
        }else{
          _this.index.bgsrc='';
        }
        
      },
      noremark(row){                       //未通过备注格式化
        if(row.Detail==''||row.Detail==null){
          return '/';
        }else{
          return row.Detail;
        }
      },
      customs(){              //客户账户
        this.index.onclickkh=true;          //按钮样式
        this.index.onclickl=false;
        this.index.onclickagent=false;
        this.index.onclickch=false;
        this.index.onclickz=false;
        this.index.onclickt=false;

        this.index.khvshow=true;              //div显示隐藏
        this.index.llfvshow=false;
        this.index.agentvshow=false;
        this.index.czvshow=false;
        this.index.zzvshow=false;
        this.index.tkvshow=false;

        var pagesize=this.index.pagesize;
        var currentpage=this.index.currentPage;
        this.GetCustomInfo(pagesize,currentpage);             //调用接口
      },
      recharge(row,type){            //充值
        var _this=this;
        _this.diakhzc.compnyname='';
        _this.diakhzc.czmoney='';
        _this.diakhzc.khvshow=true;
        _this.diakhzc.txtremark='';
        
        switch (type) {                  //判断客户还是代理商
          case '0':
            _this.diakhzc.namespan='公 司 名 称：';
            _this.diakhzc.title='客户充值';
            _this.diakhzc.id=row.CustomId;
            _this.diakhzc.compnyname=row.CompanyName;
            break;
          case '3':
            _this.diakhzc.namespan='代理商名称：';
            _this.diakhzc.title='代理商充值';
            _this.diakhzc.id=row.AgentId;
            _this.diakhzc.compnyname=row.AgentName;
            break;
          default:
            break;
        }
        
        if(row.EstablishedType!=''||row.EstablishedType!=null){
          switch (row.EstablishedType) {
            case 0:
              _this.diakhzc.opentype='自主开户';
              break;
            case 1:
              _this.diakhzc.opentype='代理商';
              break;
            case 2:
              _this.diakhzc.opentype='销售';
              break;
            default:
              break;
          }
        }else{
          _this.diakhzc.opentype='代理商';
        }

        _this.diakhzc.diakhzc=true;
      },
      refund(row,type){                 //退款
        var _this=this;
        _this.diarefund.companybank='';
        _this.diarefund.companycardnum='';
        _this.diarefund.khczvshow=true;
        _this.diarefund.responseurl='';
        _this.diarefund.picurl='';

        switch (type) {                  //判断客户还是代理商
          case '1':
            _this.diarefund.namespan='公 司 名 称：';
            _this.diarefund.title='客户退款';
            _this.diarefund.id=row.CustomId;
            _this.diarefund.companyname=row.CompanyName;
            break;
          case '4':
            _this.diarefund.namespan='代理商名称：';
            _this.diarefund.title='代理商退款';
            _this.diarefund.id=row.AgentId;
            _this.diarefund.companyname=row.AgentName;
            break;
          default:
            break;
        }

        if(row.EstablishedType!=''||row.EstablishedType!=null){
          switch (row.EstablishedType) {
            case 0:
              _this.diarefund.opentype='自主开户';
              break;
            case 1:
              _this.diarefund.opentype='代理商';
              break;
            case 2:
              _this.diarefund.opentype='销售';
              break;
            default:
              break;
          }
        }else{
          _this.diarefund.opentype='代理商';
        }

        _this.diarefund.index=true;
      },
      flows(){                //流量方账户
        this.index.onclickkh=false;              //按钮样式
        this.index.onclickl=true;
        this.index.onclickagent=false;
        this.index.onclickch=false;
        this.index.onclickz=false;
        this.index.onclickt=false;

        this.index.khvshow=false;                  //div显示隐藏
        this.index.llfvshow=true;
        this.index.agentvshow=false;
        this.index.czvshow=false;
        this.index.zzvshow=false;
        this.index.tkvshow=false;

        var pagesize=this.index.pagesize;
        var currentpage=this.index.currentPage;
        this.GetFlowsInfo(pagesize,currentpage);             //调用接口
      },
      flowsmoney(row){                  //流量方未结算金额
        var _this=this;
        var money=row.TotalIncome-row.SettledMoney;
        return money;
      },
      agents(){                 //代理商账户
        this.index.onclickkh=false;          //按钮样式
        this.index.onclickl=false;
        this.index.onclickagent=true;
        this.index.onclickch=false;
        this.index.onclickz=false;
        this.index.onclickt=false;

        this.index.khvshow=false;             //控制层
        this.index.llfvshow=false;
        this.index.agentvshow=true;
        this.index.czvshow=false;
        this.index.zzvshow=false;
        this.index.tkvshow=false;

        var pagesize=this.index.pagesize;
        var currentpage=this.index.currentPage;
        this.GetAgentInfo(pagesize,currentpage);
      },
      pays(){                 //充值记录
        this.index.onclickkh=false;            //按钮样式
        this.index.onclickl=false;
        this.index.onclickagent=false;
        this.index.onclickch=true;
        this.index.onclickz=false;
        this.index.onclickt=false;

        this.index.khvshow=false;              //控制层
        this.index.llfvshow=false;
        this.index.agentvshow=false;
        this.index.czvshow=true;
        this.index.zzvshow=false;
        this.index.tkvshow=false;

        var pagesize=this.index.pagesize;
        var currentpage=this.index.currentPage;
        this.GetRechargeInfo(pagesize,currentpage);                 //获取充值记录
      },
      details(row,type){                        //充值记录详情
        var _this=this;
        _this.GetImage(row.TransicationKey);
        if(type=='0'||type=='1'||type=='2'){
          _this.diadetail.imgurl=_this.index.bgsrc;
          _this.diadetail.remark=row.MarkText;
          if(type=='0'){             //充值记录详情
            _this.diadetail.title='充值记录详情';
          }else if(type=='1'){            //转账记录详情
            _this.diadetail.title='转账记录详情';
          }else if(type=='2'){              //退款记录详情
            _this.diadetail.title='退款记录详情';
          }
          _this.diadetail.index=true;
        }else{                       //发票详情
          _this.diapply.title='发票详情';
          _this.$http.post('/Admin/Finance/GetCustomAdderssInfo?InvoiceId='+row.InvoiceId).then(function(response){
            var info=response.data.data;
            if(info!=''||info!=null){
              _this.diapply.bill=info.InvoiceType;           //模态框赋值
              _this.diapply.inavoicename=info.InvoiceName;
              _this.diapply.taxpayernumber=info.TaxpayerNumber;
              _this.diapply.companyaddr=info.CompanyAddress;
              _this.diapply.companyphone=info.CompanyPhone;
              _this.diapply.companyback=info.CompanyBank;
              _this.diapply.companycardnum=info.CompanyCardNum;
              _this.diapply.name=info.Name;
              _this.diapply.telnumber=info.TelNumber;
              _this.diapply.email=info.Email;
              _this.diapply.address=info.Address;
              if(info.InvoiceStatus=='Open'){
                _this.diapply.express=info.ExpressType;
                _this.diapply.couriernumber=info.CourierNumber;
              }
              _this.diapply.index=true;
            }else{
              return;
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
      detailimg(){                //充值记录详情查看流水单
        var _this=this;
        _this.index.bgif=true;
      },
      cztime(row){                   //充值记录时间格式化
        if(row.ApplyDate!=''||row.ApplyDate!=null){
          var time=row.ApplyDate.replace('T',' ');
          time=time.split('.')[0];
          row.ApplyDate=time;
          return time;
        }
      },
      transfers(){                 //转账记录
        this.index.onclickkh=false;                //按钮样式
        this.index.onclickl=false;
        this.index.onclickagent=false;
        this.index.onclickch=false;
        this.index.onclickz=true;
        this.index.onclickt=false;
 
        this.index.khvshow=false;                  //控制层
        this.index.llfvshow=false;
        this.index.agentvshow=false;
        this.index.czvshow=false;
        this.index.zzvshow=true;
        this.index.tkvshow=false;

        var pagesize=this.index.pagesize;
        var currentpage=this.index.currentPage;
        this.GetTransferInfo(pagesize,currentpage);           //获取转账记录
      },
      refunds(){                     //退款记录
        this.index.onclickkh=false;
        this.index.onclickl=false;
        this.index.onclickagent=false;
        this.index.onclickch=false;
        this.index.onclickz=false;
        this.index.onclickt=true;

        this.index.khvshow=false;
        this.index.llfvshow=false;
        this.index.agentvshow=false;
        this.index.czvshow=false;
        this.index.zzvshow=false;
        this.index.tkvshow=true;

        var pagesize=this.index.pagesize;
        var currentpage=this.index.currentPage;
        this.GetRefundsInfo(pagesize,currentpage);         //获取退款记录
      },
      aready(){               //待开发票
        this.applys.onclickldai=true;
        this.applys.onclickredy=false;

        var pagesize=this.index.pagesize;
        var currentpage=this.index.currentPage;
        this.GetInavoiceInfo(pagesize,currentpage,'0');     //获取待开发票
      },
      ready(){                  //已开发票
        this.applys.onclickldai=false;
        this.applys.onclickredy=true;

        var pagesize=this.index.pagesize;
        var currentpage=this.index.currentPage;
        this.GetInavoiceInfo(pagesize,currentpage,'1');          //获取已开发票
      },
      khczreback(){
        this.diakhzc.diakhzc=false;
        this.diakhzc.khczpic='';
        this.diakhzc.czmoney='';
        this.diakhzc.txtremark='';
        this.diakhzc.khczvshow=true;
      },
      diakhczsure(){                 //确认充值
        var reg = /(^[1-9]([0-9]+)?(\.[0-9]{1,2})?$)|(^(0){1}$)|(^[0-9]\.[0-9]([0-9])?$)/;
        var _this=this;
        if(this.diakhzc.czmoney==''){
          this.$message({
            message:'请输入充值金额',
            type:'warning'
          });
          this.$refs.money.focus();
          return;
        }else if(!reg.test(this.diakhzc.czmoney)){
          this.$message({
            message:'请输入正确的充值金额',
            type:'warning'
          });
          this.$refs.money.focus();
          return;
        }else if(this.diakhzc.khczpic==''){
          this.$message({
            message:'请上传流水单',
            type:'warning'
          });
          return;
        }else{
          var type=0;
          if(_this.index.khvshow==true){
            type=0;
          }else if(_this.index.agentvshow==true){
            type=3;
          }
          
          _this.Unauthorize(0,_this.diakhzc.id,_this.diakhzc.responseurl,_this.diakhzc.czmoney,type,_this.diakhzc.txtremark);
        }
      },
      applyreback(){          //开票申请取消
        this.diapply.index=false;
        this.diapply.bill='';
        this.diapply.express='';
        this.diapply.inavoicename='';
        this.diapply.taxpayernumber='';
        this.diapply.companyaddr='';
        this.diapply.companyphone='';
        this.diapply.companyback='';
        this.diapply.companycardnum='';
        this.diapply.name='';
        this.diapply.telnumber='';
        this.diapply.email='';
        this.diapply.address='';
        this.diapply.couriernumber='';
      },
      diapplysure(){        //开票申请确认

      },
      grant(){               //待授权查看流水单
        this.index.bgif=true;
        this.index.bgsrc=this.pending.grantsrc;
      },
      grantreback(){              //待授权取消
        this.pending.txtremark='';
        this.pending.index=false;
      },
      grantsure(){            //待授权确认
        var _this=this;
        var type=_this.pending.typevalue;
        _this.authorizationAgree(_this.pending.ApplyDate,_this.pending.ApplyPerson,_this.pending.companyname,type,_this.pending.TransicationKey,_this.pending.id,_this.pending.orderid,_this.pending.Money,_this.pending.txtremark);
      },
      refundsure(){                  //退款确认
        var _this=this;
        var regnum = /(^[1-9]([0-9]+)?(\.[0-9]{1,2})?$)|(^(0){1}$)|(^[0-9]\.[0-9]([0-9])?$)/;    //金额正则校验
        if(_this.diarefund.refundmoney==''){
          _this.$message({
            message:'请输入退款金额',
            type:'warning'
          });
          _this.$refs.recompanymoney.focus();
          return;
        }else if(_this.diarefund.picurl==''){
          _this.$message({
            message:'请上传流水单',
            type:'warning'
          });
          return;
        }else if(!regnum.test(parseFloat(_this.diarefund.refundmoney))){
          _this.$message({
            message:'请输入正确的退款金额',
            type:'warning'
          });
          return;
        }else if(parseInt(_this.diarefund.refundmoney)<=0){
          _this.$message({
            message:'请输入正确的退款金额',
            type:'warning'
          });
          return;
        }else{
          var type=0;
          if(_this.index.khvshow==true){
            type=1;
          }else if(_this.index.agentvshow==true){
            type=4;
          }

          _this.Unauthorize(0,_this.diarefund.id,_this.diarefund.responseurl,_this.diarefund.refundmoney,type,_this.diarefund.txtremark);
        }
      },
      handleSizeChange(val) {    //分页功能
        var _this=this;
        _this.index.pagesize=val;
        _this.index.currentPage=1;
        var pagesize=_this.index.pagesize;
        var currentpage=_this.index.currentPage;
        if(_this.financial.index===true&&_this.index.khvshow===true){   //客户账户
          _this.GetCustomInfo(pagesize,currentpage);
        }else if(_this.financial.index===true&&_this.index.llfvshow===true){   //流量方账户
          _this.GetFlowsInfo(pagesize,currentpage);
        }else if(_this.financial.index===true&&_this.index.agentvshow===true){     //代理商账户
          _this.GetAgentInfo(pagesize,currentpage);
        }else if(_this.financial.index===true&&_this.index.czvshow===true){     //充值记录
          _this.GetRechargeInfo(pagesize,currentpage);
        }else if(_this.financial.index===true&&_this.index.zzvshow===true){     //转账记录
          _this.GetTransferInfo(pagesize,currentpage);
        }else if(_this.financial.index===true&&_this.index.tkvshow===true){     //退款记录
          _this.GetRefundsInfo(pagesize,currentpage);
        }else if(_this.applys.index===true){     //开票申请
          if(_this.applys.onclickldai==true){            //待开发票
            _this.GetInavoiceInfo(pagesize,currentpage,'0'); 
          }else if(_this.applys.onclickredy==true){          //已开发票
            _this.GetInavoiceInfo(pagesize,currentpage,'1');
          }
        }else if(_this.wait.index===true){     //待授权
          _this.GetUnauthorizedInfo(pagesize,currentpage);
        }else if(_this.nopass.index===true){     //未通过
          _this.GetNotauthorizedInfo(pagesize,currentpage);
        }
      },
      handleCurrentChange(val) {    //当前页改变
        var _this=this;
        _this.index.currentPage=val;
        var pagesize=_this.index.pagesize;
        var currentpage=_this.index.currentPage;
        if(_this.financial.index===true&&_this.index.khvshow===true){   //客户账户
          _this.GetCustomInfo(pagesize,currentpage);
        }else if(_this.financial.index===true&&_this.index.llfvshow===true){   //流量方账户
          _this.GetFlowsInfo(pagesize,currentpage);
        }else if(_this.financial.index===true&&_this.index.agentvshow===true){     //代理商账户
          _this.GetCustomInfo(pagesize,currentpage);
        }else if(_this.financial.index===true&&_this.index.czvshow===true){     //充值记录
          _this.GetRechargeInfo(pagesize,currentpage);
        }else if(_this.financial.index===true&&_this.index.zzvshow===true){     //转账记录
          _this.GetTransferInfo(pagesize,currentpage);
        }else if(_this.financial.index===true&&_this.index.tkvshow===true){     //退款记录
          _this.GetRefundsInfo(pagesize,currentpage);
        }else if(_this.applys.index===true){     //开票申请
          if(_this.applys.onclickldai==true){            //待开发票
            _this.GetInavoiceInfo(pagesize,currentpage,'0'); 
          }else if(_this.applys.onclickredy==true){          //已开发票
            _this.GetInavoiceInfo(pagesize,currentpage,'1');
          }
        }else if(_this.wait.index===true){     //待授权
          _this.GetUnauthorizedInfo(pagesize,currentpage);
        }else if(_this.nopass.index===true){     //未通过
          _this.GetNotauthorizedInfo(pagesize,currentpage);
        }
      },
      handleSizeChanges(val) {    //分页功能
        this.applys.pagesize=val;
      },
      handleCurrentChanges(val) {    //当前页改变
        var _this=this;
        
      },
      handleClosecz(done) {               //充值对话框
        this.$confirm('确认关闭？')
          .then(_ => {
            done();
          })
          .catch(_ => {});
      },
      khczsuccess(response,file,fileList){    //充值流水单上传成功的钩子
        if(response.error!=0){
          this.$message({
            message:response.message,
            type:'warning'
          });
          
          this.diakhzc.khczvshow=true;
          this.diakhzc.khczpic='';
          this.index.bgsrc='';
        }else{
          this.$message({
            message:'上传成功',
            type:'success'
          });
          this.diakhzc.khczvshow=false;
          this.diakhzc.khczpic=file.url;
          this.diakhzc.responseurl=response.url;
          this.index.bgsrc=file.url;
        }
      },
      refundsuccess(response,file,fileList){    //退款流水单上传成功的钩子
        if(response.error!=0){
          this.$message({
            message:response.message,
            type:'warning'
          });
          this.diarefund.khczvshow=true;
          this.diarefund.picurl='';
          this.index.bgsrc='';
        }else{
          this.$message({
            message:'上传成功',
            type:'success'
          });
          this.diarefund.khczvshow=false;
          this.diarefund.picurl=file.url;
          this.diarefund.responseurl=response.url;
          this.index.bgsrc=file.url;
        }
      },
      bgclick(){           //点击图片遮罩关闭
        this.index.bgif=false;
      },
      khpictr(type){           //点击查看大图
        this.index.bgif=true;
        if(type=='0'){                  //充值

        }else if(type=='1'){                 //退款

        }
      },
    },
    created(){
      var _this=this;
      _this.GetIndexInfo();      //首页信息初始化

      var pagesize=_this.index.pagesize;
      var currentpage=_this.index.currentPage;
      _this.GetCustomInfo(pagesize,currentpage);           //客户信息初始化
    },
  }
</script>

<style scoped>

</style>

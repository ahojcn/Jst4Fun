<template>
  <div>
    <div style="  text-align: center;
  margin: 0 auto;
  width: 500px;">
      <el-button type="danger" @click="$router.push('/')">退出登录</el-button>
    </div>

    <el-row :gutter="20">
      <el-col :span="8">
        <div>
          <el-card>
            <h1>竞赛发布</h1>
            <el-form :v-model="match_form">
              <el-form-item label="竞赛标题">
                <el-input v-model="match_form.title"></el-input>
              </el-form-item>
              <el-form-item label="竞赛描述">
                <el-input v-model="match_form.description"></el-input>
              </el-form-item>
              <el-form-item label="竞赛详情">
                <el-input v-model="match_form.detail"></el-input>
              </el-form-item>
              <el-form-item label="竞赛开始时间">
                <el-date-picker
                  v-model="match_form.start_time"
                  type="datetime"
                  placeholder="选择日期时间"
                ></el-date-picker>
              </el-form-item>
              <el-form-item label="竞赛结束时间">
                <el-date-picker v-model="match_form.end_time" type="datetime" placeholder="选择日期时间"></el-date-picker>
              </el-form-item>
              <el-form-item label="是否需要上传作品">
                <el-switch v-model="match_form.need_upload" active-text="需要" inactive-text="不需要"></el-switch>
              </el-form-item>
              <el-form-item>
                <el-button type="primary">发布</el-button>
              </el-form-item>
            </el-form>

            <h1>新闻发布</h1>
            <el-form :model="new_form">
              <el-form-item label="新闻标题">
                <el-input v-model="new_form.title"></el-input>
              </el-form-item>
              <el-form-item label="新闻详情">
                <el-input v-model="new_form.detail"></el-input>
              </el-form-item>
              <el-form-item>
                <el-button type="primary">发布</el-button>
              </el-form-item>
            </el-form>
          </el-card>
        </div>
      </el-col>
      <el-col :span="8">
        <div>
          <el-card>
            <h1>竞赛信息</h1>
            <el-collapse accordion>
              <el-collapse-item v-for="(item, index) in matchs" :key="index" :name="index">
                <div slot="title">
                  {{item.match.title}}【{{item.match.description}}】
                  <span>
                    <el-tag v-if="item.match.is_active">进行中</el-tag>
                    <el-tag v-if="item.match.is_active === false" type="info">已结束</el-tag>
                  </span>
                </div>

                <div>
                  <el-table
                    :row-class-name="tableRowClassName"
                    :data="item.stus"
                    style="width: 100%"
                  >
                    <el-table-column prop="name" label="姓名"></el-table-column>
                    <el-table-column prop="awards" label="获奖"></el-table-column>
                    <el-table-column label="操作">
                      <template slot-scope="scope">
                        <el-button size="mini" type="text">评奖</el-button>
                        <el-button size="mini" type="text">下载证书</el-button>
                      </template>
                    </el-table-column>
                  </el-table>
                </div>
              </el-collapse-item>
            </el-collapse>
          </el-card>
        </div>
      </el-col>
      <el-col :span="8">
        <div>
          <el-card>
            <h1>统计分析</h1>

            <div>
              已完成比赛场次/比赛总场次
              <el-progress
                :stroke-width="26"
                :percentage="parseInt(ana.m_is_active_cnt / ana.m_cnt * 100)"
              ></el-progress>
            </div>

            <div>
              参赛学生数/学生总数
              <el-progress type="circle" :stroke-width="20" :percentage="parseInt(ana.stu_match_cnt / ana.stu_cnt * 100)"></el-progress>
            </div>

            <div>
              获奖人数/参赛总数
              <el-progress type="circle" :stroke-width="20" :percentage="parseInt(ana.awards_cnt / ana.stu_match_cnt * 100)"></el-progress>
            </div>

            <div id="charts" style="width: 500px; height: 400px"></div>
          </el-card>
        </div>
      </el-col>
    </el-row>
  </div>
</template>

<script>
import { admin_match, admin_analysis } from "../api/admin";

// 基于准备好的dom，初始化echarts实例
var echarts = require("echarts");

export default {
  name: "AdminIndex",
  data() {
    return {
      match_form: {
        aid: 1,
        title: "",
        description: "",
        detail: "",
        start_time: "",
        end_time: "",
        create_time: "1592123505240",
        need_upload: true
      },

      new_form: {
        title: "",
        detail: ""
      },

      matchs: {},

      ana: {}
    };
  },
  methods: {
    tableRowClassName({ row, row_index }) {
      if (row.is_awards === true) {
        return "success-row";
      }
      return "";
    }
  },
  mounted() {
    var myChart = echarts.init(document.getElementById("charts"));
    admin_match().then(res => {
      this.matchs = res.data;
    });

    admin_analysis().then(res => {
      console.log(res.data);
      this.ana = res.data;

      // 绘制图表
      myChart.setOption({
        tooltip: {},
        xAxis: {
          data: [
            "比赛总场次",
            "正在进行场次",
            "获奖人数",
            "获奖男生数",
            "参与学生数",
            "学生总数"
          ]
        },
        yAxis: {},
        series: [
          {
            name: "人/次",
            type: "bar",
            data: [
              this.ana.m_cnt,
              this.ana.m_is_active_cnt,
              this.ana.awards_cnt,
              this.ana.awards_man,
              this.ana.stu_cnt,
              this.ana.stu_match_cnt
            ]
          }
        ]
      });
    });
  }
};
</script>

<style scoped>
.el-form-item {
  width: 100%;
}

.el-table .success-row {
  background: #f0f9eb;
}
</style>
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
            <h1>新闻公告</h1>
            <el-timeline>
              <el-timeline-item
                v-for="(item, index) in news"
                :key="index"
                :timestamp="new Date(parseInt(item.create_time)).toLocaleDateString() + ' ' + new Date(parseInt(item.create_time)).toLocaleTimeString()"
                placement="top"
                type="primary"
              >
                <el-card>
                  <h4>{{item.title}}</h4>
                  <p>{{item.detail}}</p>
                </el-card>
              </el-timeline-item>
            </el-timeline>
          </el-card>
        </div>
      </el-col>
      <el-col :span="8">
        <div>
          <el-card>
            <h1>全部比赛</h1>
            <el-collapse accordion>
              <el-collapse-item v-for="(item, index) in matchs" :key="index" :name="index">
                <div slot="title">
                  <span>{{item.title}}</span>
                  <span>【{{ item.description }}】</span>
                  <span>
                    <el-tag v-if="item.is_active">进行中</el-tag>
                    <el-tag v-if="item.is_active === false" type="info">已结束</el-tag>
                  </span>
                </div>
                <div>比赛时间：{{ new Date(parseInt(item.create_time)).toLocaleDateString() }} - {{new Date(parseInt(item.end_time)).toLocaleDateString()}}</div>
                <div>比赛详情：{{ item.detail }}</div>

                <div>
                  <el-button :disabled="item.is_active===false" type="primary" size="small" @click="handleJoin(item)">我要参加</el-button>
                </div>
              </el-collapse-item>
            </el-collapse>
          </el-card>
        </div>
      </el-col>
      <el-col :span="8">
        <div>
          <el-card>
            <h1>我的竞赛</h1>
            <el-timeline>
              <el-timeline-item
                v-for="(item, index) in mymatch"
                :key="index"
                :timestamp="new Date(parseInt(item.create_time)).toLocaleDateString() + ' ' + new Date(parseInt(item.create_time)).toLocaleTimeString()"
                placement="top"
                type="primary"
              >
                <el-card>
                  <div>
                    <span style="font-size: 20px">{{item.title}}</span>
                    <span>
                      <el-tag v-if="item.is_active">进行中</el-tag>
                      <el-tag v-if="item.is_active === false" type="info">已结束</el-tag>
                    </span>
                  </div>
                  <p>{{item.awards}}</p>

                  <el-button type="primary" size="small" @click="showDetail(item)">竞赛详情</el-button>
                </el-card>
              </el-timeline-item>
            </el-timeline>
          </el-card>
        </div>
      </el-col>
    </el-row>

    <el-drawer direction="ltr" :visible.sync="match_detail_drawer">
      <div slot="title">
        <span>{{match_detail.title}}</span>
        <span>
          <el-tag v-if="match_detail.is_active">进行中</el-tag>
          <el-tag v-if="match_detail.is_active === false" type="info">已结束</el-tag>
        </span>
      </div>
      <div>比赛时间：{{ new Date(parseInt(match_detail.create_time)).toLocaleDateString() }} - {{new Date(parseInt(match_detail.end_time)).toLocaleDateString()}}</div>
      <div>比赛描述：{{match_detail.description}}</div>
      <div>比赛详情：{{match_detail.detail}}</div>
    </el-drawer>
  </div>
</template>

<script>
import { stu_news, stu_match, stu_match_post, stu_mymatch } from "../api/stu";

export default {
  name: "StuIndex",
  data() {
    return {
      news: [],
      matchs: [],
      mymatch: [],

      match_detail: {},
      match_detail_drawer: false
    };
  },
  methods: {
    showDetail(item) {
      this.match_detail = item;
      this.match_detail_drawer = true;
    },

    // 报名比赛
    handleJoin(item) {
        let stu = JSON.parse(localStorage.getItem("stu"));
        stu_match_post({
            sid: stu.id,
            mid: item.id
        }).then(res=>{
        })
    }
  },
  mounted() {
    stu_news().then(res => {
      this.news = res.data;
    });

    stu_match().then(res => {
      this.matchs = res.data;
    });

    let stu = JSON.parse(localStorage.getItem("stu"));
    stu_mymatch(stu.id).then(res => {
      this.mymatch = res.data;
    });
  }
};
</script>

<style  scoped>
</style>
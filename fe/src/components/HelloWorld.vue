<template>
  <div class="hello">
    <h1>{{ msg }}</h1>
    <h2>基于 ASP.NET 的学科竞赛网站</h2>
    <a href="#">软件1702班 焦锡亚 41709050230</a>
    <br/>
    <hr style="width: 300px"/>

    <el-button type="primary" @click="$router.push('StuLogin')">学生入口</el-button>
    <el-button plain type="primary" @click="$router.push('AdminLogin')">管理入口</el-button>
  </div>
</template>

<script>
export default {
  name: 'HelloWorld',
  data () {
    return {
      msg: 'ASP.NET 课程设计'
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.hello{
font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}

h1, h2 {
  font-weight: normal;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>

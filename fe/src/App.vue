<template>
  <div id="app">
    <div class="backtoindex">
      <a href="/">返回首页</a>
    </div>

    
    <router-view/>
  </div>
</template>

<script>
// import {stu_login} from './api/stu'

export default {
  name: 'App',
  mounted() {
    // stu_login({
    //   stu_id: "123",
    //   pwd: "123"
    // }).then(res=>{console.log(res), err=>{console.log(err)}})
  },
}
</script>

<style>
.el-form-item {
  text-align: center;
  margin: 0 auto;
  width: 500px;
}

a {
  color: #42b983;
}

.backtoindex{
font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>

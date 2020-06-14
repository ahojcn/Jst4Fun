<template>
  <div>
    <el-form :model="loginform">
      <el-form-item>
        <h1>管理员登录</h1>
      </el-form-item>
      <el-form-item label="用户名">
        <el-input v-model="loginform.name"></el-input>
      </el-form-item>
      <el-form-item label="密码">
        <el-input v-model="loginform.pwd" show-password></el-input>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" @click="handleLoginClick">登录</el-button>
        <el-button type="text" @click="$router.push('FindPwd')">忘记密码？</el-button>
      </el-form-item>
    </el-form>
  </div>
</template>

<script>
export default {
  name: "AdminLogin",
  data() {
    return {
      loginform: {
        name: "41709310119",
        pwd: "123"
      }
    };
  },
  methods: {
    handleLoginClick() {
      this.$router.push('AdminIndex')
    }
  }
};
</script>

<style lang="" scoped>
</style>
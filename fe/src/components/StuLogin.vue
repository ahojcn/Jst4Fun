<template>
  <div>
    <el-form :model="loginform">
      <el-form-item>
        <h1>学生登录</h1>
      </el-form-item>
      <el-form-item label="学号">
        <el-input v-model="loginform.stu_id"></el-input>
      </el-form-item>
      <el-form-item label="密码">
        <el-input v-model="loginform.pwd" show-password></el-input>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" @click="handleLoginClick">登录</el-button>
        <el-button type="primary" plain @click="$router.push('StuRegister')">注册</el-button>
        <el-button type="text" @click="$router.push('FindPwd')">忘记密码？</el-button>
      </el-form-item>
    </el-form>
  </div>
</template>

<script>
import { stu_login } from "../api/stu";

export default {
  name: "Login",
  data() {
    return {
      loginform: {
        stu_id: "41709310119",
        pwd: "123"
      }
    };
  },
  methods: {
    handleLoginClick() {
      console.log(this.loginform);
      stu_login(this.loginform).then(res => {
        if (res.status === 0) {
          this.$router.push("StuIndex");
        }
        localStorage.setItem("stu", JSON.stringify(res.data));
      });
    }
  }
};
</script>

<style scoped>
</style>
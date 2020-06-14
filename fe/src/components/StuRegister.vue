<template>
  <div>
    <el-form :model="form">
      <el-form-item>
        <h1>学生注册</h1>
      </el-form-item>
      <el-form-item label="学号">
        <el-input v-model="form.stu_id"></el-input>
      </el-form-item>
      <el-form-item label="姓名">
        <el-input v-model="form.name"></el-input>
      </el-form-item>
      <el-form-item label="昵称">
        <el-input v-model="form.nick_name"></el-input>
      </el-form-item>
      <el-form-item label="性别">
        <el-select v-model="form.gender" placeholder="请选择">
          <el-option
            v-for="item in gender"
            :key="item.value"
            :label="item.label"
            :value="item.value"
          ></el-option>
        </el-select>
      </el-form-item>
      <el-form-item label="邮箱">
        <el-input v-model="form.email"></el-input>
      </el-form-item>
      <el-form-item label="手机号">
        <el-input v-model="form.phone"></el-input>
      </el-form-item>
      <el-form-item label="密码">
        <el-input v-model="form.pwd" show-password></el-input>
      </el-form-item>
      <el-form-item label="重复密码">
        <el-input v-model="form.cpwd" show-password></el-input>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" @click="handleRegisterClick">注册</el-button>
        <el-button type="primary" plain @click="$router.push('StuLogin')">登录</el-button>
      </el-form-item>
    </el-form>
  </div>
</template>

<script>
import {stu_register} from '../api/stu'

export default {
  name: "StuRegister",
  data() {
    return {
      form: {
        stu_id: "",
        name: "",
        nick_name: "",
        gender: 1,
        email: "",
        phone: "",
        pwd: "",
        cpwd: ""
      },
      gender: [
        {
          value: 1,
          label: "男生"
        },
        {
          value: 2,
          label: "女生"
        }
      ]
    };
  },
  methods: {
      handleRegisterClick() {
          stu_register(this.form).then(res=>{
              if (res.status === 0) {
                  this.$router.push('StuLogin')
              }
          }, err=>{})
      }
  }
};
</script>

<style lang="" scoped>
</style>
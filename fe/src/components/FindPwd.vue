<template>
    <div>
        <el-form :model="form">
        <el-form-item>
          <h1>找回密码</h1>
        </el-form-item>
        <el-form-item label="邮箱">
          <el-input v-model="form.email"></el-input>
        </el-form-item>

        <el-form-item>
          <el-button type="primary">1. 发送验证码</el-button>
        </el-form-item>

        <el-form-item label="验证码">
          <el-input v-model="form.code" show-password></el-input>
        </el-form-item>
        <el-form-item>
          <el-button type="primary">2. 验证</el-button>
        </el-form-item>

        <el-form-item label="新密码">
          <el-input v-model="form.newpwd" show-password></el-input>
        </el-form-item>
        <el-form-item>
          <el-button type="primary">3. 输入新密码</el-button>
        </el-form-item>
      </el-form>
    </div>
</template>

<script>
    export default {
        name: '',
        data() {
            return {
                form: {
                    is_stu: false,
                    email: "",
                    code: "",
                    newpwd: ""
                }
            }
        }
    }
</script>

<style lang="" scoped>
    
</style>
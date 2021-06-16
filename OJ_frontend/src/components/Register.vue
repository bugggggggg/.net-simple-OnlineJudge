<template>
  <div class="page">

    <el-card class="box" shadow="always">
      <el-row class="title">
        <h2>注册</h2>
      </el-row>
      <el-form label-width="0">
        <el-form-item>
          <i class="el-icon-user"></i>
          <el-input class="form-input" v-model="username" placeholder="用户名">
          </el-input>
        </el-form-item>

        <el-form-item>
          <i class="el-icon-s-goods"></i>
          <el-input class="form-input" v-model="password" placeholder="密码" show-password>
          </el-input>
        </el-form-item>

        <el-form-item >
          <i class="el-icon-s-goods"></i>
          <el-input class="form-input" v-model="confirmPassword" placeholder="确认密码" show-password>
          </el-input>
        </el-form-item>



        <el-form-item class="register">
          <el-button type="primary" class="form-button" v-on:click="register" round>注册</el-button>
          <el-button type="primary" class="form-button" v-on:click="toLogin" round>去登陆</el-button>
        </el-form-item>
      </el-form>
    </el-card>
  </div>
</template>

<script>
export default {
  name: 'Register',
  data() {
    return {
      username:'',
      password:'',
      confirmPassword:'',

    }
  },
  mounted() {
    document.querySelector('body').setAttribute('style', 'background-color: aliceblue')
  },
  beforeDestroy() {
    document.querySelector('body').removeAttribute('style')
  },
  methods:{


    register:function() {
      if( this.username == "" || this.password == "") {
        alert("请正确输入");
      }

        if(this.password === this.confirmPassword){

          const url = this.APi;
          this.$axios.post(url + 'api/account/register',
              { "Username":this.username,
                "Password":this.password,
              })
              .then(function (response) {
                const Data = response.data[0];
                if(response.status === 200&&Data.status === 200){
                     alert("注册成功!");
                     location="./login"

                   } else{
                     console.log("注册失败");
                     const msg = Data.msg;
                     alert("注册失败" + msg);
                   }
              })

              .catch(function (error) { // 请求失败处理
                console.log(error);
              });

        }else{
          console.log("两次密码不一致!");
          alert("两次密码不一致!");
        }

    },

    toLogin:function(){
      location="./login";
    }


  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.page {
  width: 100%;
  /* height: 100%;
  background-color: aliceblue; */
}

.box {
  width: 30%;
  margin-top: 100px;
  margin-left: 35%;
}

.title {
  text-align: center;
}

.form-input {
  width: 80%;
}

.verifyCode {
  width: 55%;
}

#verifyCode {
  margin-right: 10%;
}

.el-icon-user {
  font-size: 25px;
  margin: 0 20px;
}

.el-icon-s-goods {
  font-size: 25px;
  margin: 0 20px;
}

.el-icon-message {
  font-size: 25px;
  margin: 0 20px;
}

.el-icon-position {
  font-size: 25px;
  margin: 0 20px;
}

.btnCode {
  margin-left: 15px;
  padding: 11px;
}

.register {
  text-align: center;
}
</style>

<template>
    <el-container class="home-container">

        <!-- <el-header>
          <div style="display: flex;align-items: center;">
            <img src="../assets/avater.png" alt="logo" style="width: 5%;height: 5%;">
            <span style="font-size:large;">codespace</span>
          </div>
          <el-button type="primary" v-on:click="logout">退出</el-button>
        </el-header> -->

        <!-- 侧边栏 -->
        <Sidebar/>

        <el-main>
          <Navbar/>

          <div v-show="routeName == '首页'" class="home_container">
            <div class="title">
              <h2>Online Judge</h2>
            </div>

          </div>


          <!-- 路由占位符-->
          <router-view></router-view>

        </el-main>

    </el-container>
</template>

<script>

import Sidebar from "@/components/Sidebar";
import Navbar from "@/components/Navbar";

export default {
  name: "Home",
  computed:{
    // routeName(){
    //     const route = this.$route
    //     const { meta, path } = route
    //     return meta.name
    // }
  },
  components:{
    Sidebar,
    Navbar
  },
  data(){
    return{
      // 左侧菜单
      menuList:[],


    }
  },

  methods:{
    logout:function(){
      sessionStorage.setItem("isLogin", false);
      sessionStorage.removeItem("userid" );
      //
      location="/login"

    },


  }

}
</script>

<style scoped>

.home-container{
  height: 100%;
}

.el-main {
  padding: 0;
}

.el-header{
  display: flex;
  justify-content: space-between;
  padding-left: 0;
  align-items: center;
}

.el-aside{

}

.el-main{
  /* background-color: azure; */
}


.title {
  font-weight: bold;
  text-align: center;
}

.row {
  margin-top: 40px;
}

.recommend {
  margin-top: 10px;
  margin-left: 10px;
  margin-right: 10px;
}

.match {
  margin-left: 20px
}

</style>

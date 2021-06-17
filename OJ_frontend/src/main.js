import Vue from 'vue';
import ElementUI from 'element-ui';
import 'element-ui/lib/theme-chalk/index.css';
import App from './App.vue';
import router from './router'
import store from './store'
import axios from 'axios'
import mavonEditor from 'mavon-editor'
import 'mavon-editor/dist/css/index.css'

import Api from "@/router/Api"

Vue.prototype.APi=Api.serverSrc

import './assets/css/global.css'


Vue.prototype.$axios = axios
Vue.use(ElementUI);
Vue.use(mavonEditor)
Vue.config.productionTip = false



router.beforeEach((to, from, next) => {
    /* 路由发生变化修改页面title */
    // if (to.meta.title) {
    //   document.title = to.meta.title
    // }
    // next()




    next();
})

new Vue({
    el: '#app',
    router,
    store,
    render: h => h(App)
});

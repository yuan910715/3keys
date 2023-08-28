import Vue from 'vue'
import ElementUI from 'element-ui'
import 'element-ui/lib/theme-chalk/index.css'
import App from './App.vue'
import VueI18n from 'vue-i18n'
import enLocale from 'element-ui/lib/locale/lang/en'
import zhLocale from 'element-ui/lib/locale/lang/zh-CN'
import myEn from './i18n/en'
import myZh from './i18n/zh'

Vue.use(VueI18n)

const messages = {
  en: {
    ...myEn,
    ...enLocale
  },
  zh: {
    ...myZh,
    ...zhLocale
  }
}

const i18n = new VueI18n({
  locale: localStorage.getItem('lang')||'zh',
  messages, 
})

Vue.use(ElementUI, {
  i18n: (key, value) => i18n.t(key, value)
})

new Vue({
  el: '#app',
  i18n,
  render: h => h(App)
})

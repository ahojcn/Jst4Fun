import request from './utils'

// 登录
export function admin_login(data) {
    return request({
        url: 'admin/login',
        method: 'post',
        data
    })
}

// 查看竞赛
export function admin_match(query) {
    return request({
        url: 'admin/match',
        method: 'get',
        params: query
    })
}

// 竞赛设置奖项
export function admin_awards_post(data) {
    return request({
        url: 'admin/awards',
        method: 'post',
        data
    })
}

// 生成证书
export function admin_awards(query) {
    return request({
        url: 'stu/awards',
        method: 'get',
        params: query
    })
}

// 统计分析
export function admin_analysis(query) {
    return request({
        url: 'admin/analysis',
        method: 'get',
        params: query
    })
}

// 新闻发布
export function admin_news(data) {
    return request({
        url: 'admin/news',
        method: 'post',
        data
    })
}
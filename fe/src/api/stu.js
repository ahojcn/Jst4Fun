import request from './utils'

// 学生登录
export function stu_login(data) {
    return request({
        url: 'stu/login',
        method: 'post',
        data
    })
}

// 学生注册
export function stu_register(data) {
    return request({
        url: 'stu/register',
        method: 'post',
        data
    })
}

// 获取新闻
export function stu_news(query) {
    return request({
        url: 'stu/news',
        method: 'get',
        params: query
    })
}

// 查看竞赛
export function stu_match(query) {
    return request({
        url: 'stu/match',
        method: 'get',
        params: query
    })
}

// 竞赛报名
export function stu_match_post(data) {
    return request({
        url: 'stu/match',
        method: 'get',
        data
    })
}

// 我的竞赛
export function stu_mymatch(sid) {
    let url = 'stu/mymatch?sid=' + sid;

    return request({
        url: url,
        method: 'get'
    })
}
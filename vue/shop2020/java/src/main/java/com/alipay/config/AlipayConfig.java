package com.alipay.config;

import java.io.FileNotFoundException;
import java.io.IOException;
import java.util.Properties;

/* *
 *类名：AlipayConfig
 *功能：基础配置类
 *详细：设置帐户有关信息及返回路径
 *版本：3.3
 *日期：2012-08-10
 *说明：
 *以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
 *该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
	
 *提示：如何获取安全校验码和合作身份者ID
 *1.用您的签约支付宝账号登录支付宝网站(www.alipay.com)
 *2.点击“商家服务”(https://b.alipay.com/order/myOrder.htm)
 *3.点击“查询合作者身份(PID)”、“查询安全校验码(Key)”

 *安全校验码查看时，输入支付密码后，页面呈灰色的现象，怎么办？
 *解决方法：
 *1、检查浏览器配置，不让浏览器做弹框屏蔽设置
 *2、更换浏览器或电脑，重新登录查询。
 */

public class AlipayConfig {
    public static String URL = "https://openapi.alipay.com/gateway.do";
	public static String APPID ="";
	public static String RSA_PRIVATE_KEY="";
	public static String ALIPAY_PUBLIC_KEY="";
	public static String notify_url="";
	
	//↓↓↓↓↓↓↓↓↓↓请在这里配置您的基本信息↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
	// 合作身份者ID，以2088开头由16位纯数字组成的字符串
	public static String partner = "";
	
	// 收款支付宝账号
	public static String seller_email = "";
	// 商户的私钥
	public static String key = "";
	
	public static String public_key  = "";

	//↑↑↑↑↑↑↑↑↑↑请在这里配置您的基本信息↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑

	// 调试用，创建TXT日志文件夹路径
	public static String log_path = "D:\\";

	// 字符编码格式 目前支持 gbk 或 utf-8
	public static String input_charset = "utf-8";
	
	// 签名方式 不需修改
	public static String sign_type = "RSA";
	
	public static Properties props = new Properties(); 
	
	static{
		try {
			props.load(Thread.currentThread().getContextClassLoader().getResourceAsStream("alipay.properties"));
			 if (props!=null) {
				 partner = props.getProperty("partner");
				 key = props.getProperty("key");
				 public_key = props.getProperty("public_key");
				 sign_type = props.getProperty("sign_type");
				 seller_email = props.getProperty("seller_email");
				 log_path = props.getProperty("log_path"); 
				 notify_url = props.getProperty("notify_url");
				 APPID = props.getProperty("appid");
				 RSA_PRIVATE_KEY = props.getProperty("privateKey");
				 ALIPAY_PUBLIC_KEY= props.getProperty("publicKey");
			}
		} catch (FileNotFoundException e) {
			e.printStackTrace();
		} catch (IOException e) {
			e.printStackTrace();
		}
	}

}

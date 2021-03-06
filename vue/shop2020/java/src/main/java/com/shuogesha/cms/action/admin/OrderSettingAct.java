package com.shuogesha.cms.action.admin;

import static com.shuogesha.platform.web.mongo.SimplePage.cpn;

import java.io.UnsupportedEncodingException;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.ResponseBody;
import org.springframework.web.bind.annotation.RestController;

import com.shuogesha.common.util.JsonResult;
import com.shuogesha.common.util.ResultCode;
import com.shuogesha.cms.entity.OrderSetting;
import com.shuogesha.cms.service.OrderSettingService;
import com.shuogesha.platform.web.CmsUtils;
import com.shuogesha.platform.web.mongo.Pagination;

@RestController 
@RequestMapping("/api/orderSetting/")
public class OrderSettingAct {
//	@RequestMapping(value = "/list")
//	public @ResponseBody Object v_list(String name, Integer pageNo, Integer pageSize, HttpServletRequest request,
//			HttpServletResponse response) throws UnsupportedEncodingException {
//		Pagination pagination = orderSettingService.getPage(name, cpn(pageNo), pageSize);
//		return new JsonResult(ResultCode.SUCCESS, pagination);
//	}
//
//	@RequestMapping(value = "/get")
//	public @ResponseBody Object v_get(Long id) {
//		OrderSetting bean = orderSettingService.findById(id);
//		return new JsonResult(ResultCode.SUCCESS, bean);
//	}
//
//	@RequestMapping(value = "/save", method = RequestMethod.POST)
//	public @ResponseBody Object o_save(@RequestBody OrderSetting bean) {
// 		orderSettingService.save(bean);
//		return new JsonResult(ResultCode.SUCCESS, true);
//	}

	@RequestMapping(value = "/update", method = RequestMethod.POST)
	public @ResponseBody Object o_update(@RequestBody OrderSetting bean) {
		orderSettingService.update(bean);
		return new JsonResult(ResultCode.SUCCESS, true);
	}

//	@RequestMapping(value = "/delete")
//	public @ResponseBody Object o_delete(Long[] ids) {
//		orderSettingService.removeByIds(ids);
//		return new JsonResult(ResultCode.SUCCESS, true);
//	} 
	
	@RequestMapping(value = "/config")
	public @ResponseBody Object config(HttpServletRequest request) {
		OrderSetting bean = orderSettingService.findById(CmsUtils.getSiteId(request));
		return new JsonResult(ResultCode.SUCCESS, bean);
	}
	 
	 
	@Autowired
	public OrderSettingService orderSettingService; 
}

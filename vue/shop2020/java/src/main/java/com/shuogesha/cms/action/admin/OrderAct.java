package com.shuogesha.cms.action.admin;

import static com.shuogesha.platform.web.mongo.SimplePage.cpn;

import java.io.UnsupportedEncodingException;
import java.util.Date;
import java.util.List;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.commons.lang.StringUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.ResponseBody;
import org.springframework.web.bind.annotation.RestController;

import com.alibaba.fastjson.JSON;
import com.alibaba.fastjson.JSONObject;
import com.shuogesha.cms.entity.Address;
import com.shuogesha.cms.entity.Order;
import com.shuogesha.cms.entity.OrderNote;
import com.shuogesha.cms.entity.Shipping;
import com.shuogesha.cms.service.OrderNoteService;
import com.shuogesha.cms.service.OrderService;
import com.shuogesha.cms.service.ShippingService;
import com.shuogesha.common.util.JsonResult;
import com.shuogesha.common.util.ResultCode;
import com.shuogesha.platform.version.SysLog;
import com.shuogesha.platform.web.CmsUtils;
import com.shuogesha.platform.web.mongo.Pagination;

@RestController 
@RequestMapping("/api/order/")
public class OrderAct {
	@RequestMapping(value = "/list")
	public @ResponseBody Object v_list(String name, Integer pageNo, Integer pageSize, HttpServletRequest request,
			HttpServletResponse response) throws UnsupportedEncodingException {
		Pagination pagination = orderService.getPage(name, cpn(pageNo), pageSize);
		return new JsonResult(ResultCode.SUCCESS, pagination);
	}

	@RequestMapping(value = "/get")
	public @ResponseBody Object v_get(Long id) {
		Order bean = orderService.findById(id);
		if(bean!=null&&StringUtils.isNotBlank(bean.getContent())) {
			bean.setProductJSON(JSON.parseObject(bean.getContent()));
		}
		List<OrderNote> orderNotes=orderNoteService.findList(id);
		bean.setOrderNotes(orderNotes);
		return new JsonResult(ResultCode.SUCCESS, bean);
	}

	@RequestMapping(value = "/save", method = RequestMethod.POST)
	public @ResponseBody Object oSave(@RequestBody Order bean) {
 		orderService.save(bean);
		return new JsonResult(ResultCode.SUCCESS, true);
	}

	@RequestMapping(value = "/update", method = RequestMethod.POST)
	public @ResponseBody Object o_update(@RequestBody Order bean) {
		orderService.update(bean);
		return new JsonResult(ResultCode.SUCCESS, true);
	}

	@RequestMapping(value = "/delete")
	@SysLog(description = "????????????")
	public @ResponseBody Object o_delete(Long[] ids) {
		orderService.removeByIds(ids);
		return new JsonResult(ResultCode.SUCCESS, true);
	} 
	 
	/**
	 * ????????????
	 * @param bean
	 * @return
	 */
	@RequestMapping(value = "/cancel", method = RequestMethod.POST)
	@SysLog(description = "????????????")
	public @ResponseBody Object cancel(@RequestBody Order bean, HttpServletRequest request) {
		Order order = orderService.findById(bean.getId());
		if(Order.ORDER_CANCEL.equals(order.getStatus())){ 
			return new JsonResult(ResultCode.FAIL,"???????????????",null);
		} 
		bean=orderService.cancel(order);
		//????????????
		OrderNote orderNote= new OrderNote(order.getStatus(), order.getPay(), order.getShippingStatus(), "????????????", order.getId(), CmsUtils.getUser(request));
		orderNoteService.save(orderNote);
		return new JsonResult(ResultCode.SUCCESS,bean);
	}
	/**
	 * ??????????????????
	 * @param bean
	 * @return
	 */
	@RequestMapping(value = "/pay", method = RequestMethod.POST)
	@SysLog(description = "????????????????????????")
	public @ResponseBody Object pay(@RequestBody Order bean, HttpServletRequest request) {
		Order order = orderService.findById(bean.getId());
		//???????????????
		if(Order.PAY_YIZHIFU.equals(bean.getPay())) {
			order.setPayTime(new Date());
		}
		order.setPay(bean.getPay());
		orderService.update(order);
		//????????????
		OrderNote orderNote= new OrderNote(order.getStatus(), order.getPay(), order.getShippingStatus(), "??????????????????", order.getId(), CmsUtils.getUser(request));
		orderNoteService.save(orderNote);
		return new JsonResult(ResultCode.SUCCESS,"????????????",order);
	}
	/**
	 * ????????????
	 * @param bean
	 * @return
	 */
	@RequestMapping(value = "/shipping", method = RequestMethod.POST)
	@SysLog(description = "????????????????????????")
	public @ResponseBody Object shipping(@RequestBody Order bean, HttpServletRequest request) {
 		Order order = orderService.findById(bean.getId());
 		if(order!=null&&Order.PAY_YIZHIFU.equals(order.getPay())) {
 			order.setShippingStatus(bean.getShippingStatus());
 			if("3".equals(order.getShippingStatus())) {
 				order.setStatus(Order.ORDER_OK);
 				//??????????????????
 				order.setShippingTime(new Date());
 			} 
 			orderService.update(order);
 		}
		if(order!=null&&"0".equals(order.getShippingStatus())) {
			shippingService.removeByOrderId(bean.getId()); 
		} 
		//????????????
		OrderNote orderNote= new OrderNote(order.getStatus(), order.getPay(), order.getShippingStatus(), "??????????????????", order.getId(), CmsUtils.getUser(request));
		orderNoteService.save(orderNote);
		return new JsonResult(ResultCode.SUCCESS,"????????????",order); 
	}
	/**
	 * ??????
	 * @param bean
	 * @return
	 */
	@RequestMapping(value = "/saveShipping", method = RequestMethod.POST)
	@SysLog(description = "????????????")
	public @ResponseBody Object saveShipping(@RequestBody Shipping bean, HttpServletRequest request) {
		shippingService.save(bean);
		Order order = orderService.findById(bean.getOrderId());
		if(!"2".equals(order.getShippingStatus())) {//????????????
			order.setShippingStatus("2");
			orderService.update(order);
		}
		//????????????
		OrderNote orderNote= new OrderNote(order.getStatus(), order.getPay(), order.getShippingStatus(), "????????????", order.getId(), CmsUtils.getUser(request));
		orderNoteService.save(orderNote);
		return new JsonResult(ResultCode.SUCCESS,"????????????",bean);
	}
	
	
	@RequestMapping(value = "/updateAddress", method = RequestMethod.POST)
	@SysLog(description = "????????????????????????")
	public @ResponseBody Object updateAddress(@RequestBody String str, HttpServletRequest request) {
		if(StringUtils.isBlank(str)) {
			return new JsonResult(ResultCode.FAIL, "????????????",null);
		} 
 		Order bean= JSON.parseObject(str, Order.class); //???order 
 		JSONObject json = JSONObject.parseObject(str); //??????????????????????????????
 		if(StringUtils.isBlank(json.getString("addressData"))) {
 			return new JsonResult(ResultCode.FAIL, "????????????",null);
 		} 
 		if(bean.getId()<=0) {
 			return new JsonResult(ResultCode.FAIL, "????????????",null);
 		}
 		Order order = orderService.findById(bean.getId());
		if(order!=null) {
			Address address = JSON.parseObject(json.getString("addressData"), Address.class); //???order 
			JSONObject content=JSONObject.parseObject(order.getContent());
			content.put("addressData", json.get("addressData"));
	  		if(address!=null) {
	  			order.setPhone(address.getPhone());
	  			order.setAddress(address.getAddress());
	  		}
	  		order.setContent(content.toJSONString());
	  		orderService.update(order);//????????????
		}
		//????????????
		OrderNote orderNote= new OrderNote(order.getStatus(), order.getPay(), order.getShippingStatus(), "??????????????????", order.getId(), CmsUtils.getUser(request));
		orderNoteService.save(orderNote);
		return new JsonResult(ResultCode.SUCCESS, order);
	}
	
	

	
	@Autowired
	public OrderService orderService; 
	@Autowired
	public ShippingService shippingService;
	@Autowired
	public OrderNoteService orderNoteService; 
}

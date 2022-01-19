package com.shuogesha.cms.entity;

import java.io.Serializable;
import java.math.BigDecimal;
import java.util.List;

import org.springframework.data.annotation.Transient;

import com.alibaba.fastjson.JSONArray;

public class Product implements Serializable {
	
	





	private Integer sale=0;//销售
	
	private Integer sort;



	
	private ProductCat productCat;
	
	private String dateline;
	
	private Count count;//浏览量
	
	private String attributes;//规格参数JSON
	
	private String status="0";//0上架，1下架

	private String itemParam;//产品参数JSON
	
	List<ProductAttr> productAttrList;//规格组合字段
	
	private String barCode;//条码
	
	
	@Transient
	private JSONArray imgList; //json图片集合
	
	@Transient
	private JSONArray attributesList; //规格集合
	
	private Boolean collected=false;//是否收藏 

 
		return this.id;
	}

		this.id=id;
	}

		return this.name;
	}

		this.name=name;
	}

		return this.img;
	}

		this.img=img;
	}
 

		return this.price;
	}

		this.price=price;
	}

		return this.unit;
	}

		this.unit=unit;
	}

		return this.num;
	}

		this.num=num;
	}

		return this.remark;
	}

		this.remark=remark;
	} 

		return this.imgs;
	}

		this.imgs=imgs;
	}

	public String getContent() {
		return content;
	}

	public void setContent(String content) {
		this.content = content;
	}

	public ProductCat getProductCat() {
		return productCat;
	}

	public void setProductCat(ProductCat productCat) {
		this.productCat = productCat;
	}

	public JSONArray getImgList() {
		return imgList;
	}

	public void setImgList(JSONArray imgList) {
		this.imgList = imgList;
	}

	public Integer getSort() {
		return sort;
	}

	public void setSort(Integer sort) {
		this.sort = sort;
	}

	public String getDateline() {
		return dateline;
	}

	public void setDateline(String dateline) {
		this.dateline = dateline;
	}

	public Count getCount() {
		return count;
	}

	public void setCount(Count count) {
		this.count = count;
	}

	public String getAttributes() {
		return attributes;
	}

	public void setAttributes(String attributes) {
		this.attributes = attributes;
	}

	public JSONArray getAttributesList() {
		return attributesList;
	}

	public void setAttributesList(JSONArray attributesList) {
		this.attributesList = attributesList;
	}

	public String getStatus() {
		return status;
	}

	public void setStatus(String status) {
		this.status = status;
	}

	public List<ProductAttr> getProductAttrList() {
		return productAttrList;
	}

	public void setProductAttrList(List<ProductAttr> productAttrList) {
		this.productAttrList = productAttrList;
	}

	 

	public Integer getSale() {
		return sale;
	}

	public void setSale(Integer sale) {
		this.sale = sale;
	}

	public Boolean getCollected() {
		return collected;
	}

	public void setCollected(Boolean collected) {
		this.collected = collected;
	}

	public String getItemParam() {
		return itemParam;
	}

	public void setItemParam(String itemParam) {
		this.itemParam = itemParam;
	}

	public String getBarCode() {
		return barCode;
	}

	public void setBarCode(String barCode) {
		this.barCode = barCode;
	}

	 
	
}
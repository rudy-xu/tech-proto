package com.shuogesha.platform.entity;

import java.io.Serializable;
import java.util.Date;

public class SystemLog implements Serializable {
	
	public static final String PC="PC";
	public static final String APP="APP";  
	
	private String ip;
	private String url;
	private String method;
	private String param;
	private Long userId;
	

		return this.id;
	}

		this.id=id;
	}

		return this.name;
	}

		this.name=name;
	}

		return this.username;
	}

		this.username=username;
	} 

		return this.content;
	}

		this.content=content;
	}

		return this.type;
	}

		this.type=type;
	}

	public String getIp() {
		return ip;
	}

	public void setIp(String ip) {
		this.ip = ip;
	}

	public String getUrl() {
		return url;
	}

	public void setUrl(String url) {
		this.url = url;
	}

	public String getMethod() {
		return method;
	}

	public void setMethod(String method) {
		this.method = method;
	}

	public String getParam() {
		return param;
	}

	public void setParam(String param) {
		this.param = param;
	} 

	public Long getUserId() {
		return userId;
	}

	public void setUserId(Long userId) {
		this.userId = userId;
	}

	public void setDateline(Date dateline) {
		this.dateline = dateline;
	}

	public Date getDateline() {
		return dateline;
	} 
	
	
}
package com.shuogesha.cms.entity;

import java.io.Serializable;

import org.springframework.data.annotation.Transient;

import com.alibaba.fastjson.JSONObject;
/**
 * 收藏功能
 * @author zhaohaiyuan
 *
 */
public class Collect implements Serializable {
	
	






	
	@Transient
	private JSONObject data;//收藏的json
	


    public Long getId() {
		return this.id;
	}

		this.id=id;
	}

		return this.name;
	}

		this.name=name;
	}

		return this.referid;
	}

		this.referid=referid;
	}

		return this.refer;
	}

		this.refer=refer;
	}

		return this.dateline;
	}

		this.dateline=dateline;
	} 

		return userId;
	}

	public void setUserId(Long userId) {
		this.userId = userId;
	}

	public String getContent() {
		return this.content;
	}

		this.content=content;
	}

	public JSONObject getData() {
		return data;
	}

	public void setData(JSONObject data) {
		this.data = data;
	}

	
}
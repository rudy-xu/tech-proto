package com.shuogesha.cms.entity;

import java.io.Serializable;

import com.shuogesha.platform.entity.User;

public class Content implements Serializable {
	
	





	
	private Channel channel;
	private Count count;

		return this.id;
	}

		this.id=id;
	}

		return this.name;
	}

		this.name=name;
	}

		return this.content;
	}

		this.content=content;
	}

		return this.dateline;
	}

		this.dateline=dateline;
	}

		return this.description;
	}

		this.description=description;
	}

		return this.img;
	}

		this.img=img;
	}

	public User getUser() {
		return user;
	}

	public void setUser(User user) {
		this.user = user;
	}

	public Channel getChannel() {
		return channel;
	}

	public void setChannel(Channel channel) {
		this.channel = channel;
	}

	public Count getCount() {
		return count;
	}

	public void setCount(Count count) {
		this.count = count;
	}
	
	
	
}
package com.shuogesha.platform.entity;

import java.io.Serializable;

public class Dictionary implements Serializable {
	
	
	private String name;
	private String value;
	private Integer ctgId;
	private Integer sort;

		return this.id;
	}

		this.id=id;
	}

		return this.name;
	}

		this.name=name;
	}

		return this.sort;
	}

		this.sort=sort;
	}

		return this.value;
	}

		this.value=value;
	}

	public Integer getCtgId() {
		return ctgId;
	}

	public void setCtgId(Integer ctgId) {
		this.ctgId = ctgId;
	}

}
﻿
public class Pair<T>{

	public Pair(){}

	public Pair(T left, T right){
		this.Left = left;
		this.Right = right;
	}

	public T Left{get; set;}
	public T Right{get; set;}
}

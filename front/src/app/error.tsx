"use client";

import { useEffect } from "react";
import { useRouter } from "next/navigation";

export default function Error({ error, reset }: { error: Error; reset: () => void }) {
  const router = useRouter();

  useEffect(() => {
    console.error("ページ表示中に問題が発生しました。", error);
  }, [error]);

  return (
    <div style={{ textAlign: "center", marginTop: "50px" }}>
      <h1>❌ただいま、エラーが発生しております。</h1>
      <p>ページ表示中に問題が発生しました。</p>
      <button 
        onClick={reset} 
        style={{
          marginRight: "10px",
          padding: "10px 20px",
          backgroundColor: "#0070f3",
          color: "white",
          border: "none",
          borderRadius: "5px",
          cursor: "pointer"
        }}
      >
        リトライ
      </button>
      <button 
        onClick={() => router.push("/")} 
        style={{
          padding: "10px 20px",
          backgroundColor: "#ff0000",
          color: "white",
          border: "none",
          borderRadius: "5px",
          cursor: "pointer"
        }}
      >
        ホームへ戻る
      </button>
    </div>
  );
}

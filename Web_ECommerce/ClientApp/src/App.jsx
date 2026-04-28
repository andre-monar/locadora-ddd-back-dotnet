import { useState, useEffect } from "react";
 
// ─── Paleta & estilos globais ────────────────────────────────────
const CSS = `
  @import url('https://fonts.googleapis.com/css2?family=Syne:wght@400;600;700;800&family=DM+Sans:ital,wght@0,300;0,400;0,500;1,300&display=swap');
 
  *, *::before, *::after { box-sizing: border-box; margin: 0; padding: 0; }
 
  :root {
    --bg:       #0d0f14;
    --surface:  #13161e;
    --card:     #181c27;
    --border:   rgba(255,255,255,0.07);
    --accent1:  #6c63ff;
    --accent2:  #00d4aa;
    --accent3:  #ff6b6b;
    --text:     #e8eaf0;
    --muted:    #6b7280;
    --heading:  'Syne', sans-serif;
    --body:     'DM Sans', sans-serif;
    --radius:   14px;
    --trans:    0.22s cubic-bezier(.4,0,.2,1);
  }
 
  body { background: var(--bg); color: var(--text); font-family: var(--body); min-height: 100vh; }
 
  /* scrollbar */
  ::-webkit-scrollbar { width: 5px; }
  ::-webkit-scrollbar-track { background: transparent; }
  ::-webkit-scrollbar-thumb { background: var(--border); border-radius: 99px; }
 
  @keyframes fadeUp {
    from { opacity: 0; transform: translateY(18px); }
    to   { opacity: 1; transform: translateY(0); }
  }
  @keyframes pulse-ring {
    0%   { box-shadow: 0 0 0 0 rgba(108,99,255,.45); }
    70%  { box-shadow: 0 0 0 14px rgba(108,99,255,0); }
    100% { box-shadow: 0 0 0 0 rgba(108,99,255,0); }
  }
  @keyframes shimmer {
    0%   { background-position: -400px 0; }
    100% { background-position: 400px 0; }
  }
  .skeleton {
    background: linear-gradient(90deg, var(--card) 25%, #1e2233 50%, var(--card) 75%);
    background-size: 800px 100%;
    animation: shimmer 1.4s infinite;
    border-radius: 8px;
  }
`;
 
// ─── Injeção do CSS ──────────────────────────────────────────────
const StyleTag = () => <style dangerouslySetInnerHTML={{ __html: CSS }} />;
 
// ─── Ícones SVG simples ──────────────────────────────────────────
const Icon = {
  Car: () => (
    <svg width="28" height="28" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="1.7" strokeLinecap="round" strokeLinejoin="round">
      <path d="M5 17H3a2 2 0 0 1-2-2V9a2 2 0 0 1 2-2h14l4 4v4a2 2 0 0 1-2 2h-2"/>
      <circle cx="7" cy="17" r="2"/><circle cx="17" cy="17" r="2"/>
    </svg>
  ),
  User: () => (
    <svg width="28" height="28" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="1.7" strokeLinecap="round" strokeLinejoin="round">
      <path d="M20 21v-2a4 4 0 0 0-4-4H8a4 4 0 0 0-4 4v2"/>
      <circle cx="12" cy="7" r="4"/>
    </svg>
  ),
  Rental: () => (
    <svg width="28" height="28" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="1.7" strokeLinecap="round" strokeLinejoin="round">
      <rect x="3" y="4" width="18" height="18" rx="2"/><line x1="16" y1="2" x2="16" y2="6"/>
      <line x1="8" y1="2" x2="8" y2="6"/><line x1="3" y1="10" x2="21" y2="10"/>
      <path d="M8 14h.01M12 14h.01M16 14h.01M8 18h.01M12 18h.01"/>
    </svg>
  ),
  Plus: () => (
    <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2.2" strokeLinecap="round">
      <line x1="12" y1="5" x2="12" y2="19"/><line x1="5" y1="12" x2="19" y2="12"/>
    </svg>
  ),
  Edit: () => (
    <svg width="15" height="15" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
      <path d="M11 4H4a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2v-7"/>
      <path d="M18.5 2.5a2.121 2.121 0 0 1 3 3L12 15l-4 1 1-4 9.5-9.5z"/>
    </svg>
  ),
  Trash: () => (
    <svg width="15" height="15" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
      <polyline points="3 6 5 6 21 6"/><path d="M19 6l-1 14a2 2 0 0 1-2 2H8a2 2 0 0 1-2-2L5 6"/>
      <path d="M10 11v6M14 11v6"/><path d="M9 6V4a1 1 0 0 1 1-1h4a1 1 0 0 1 1 1v2"/>
    </svg>
  ),
  Back: () => (
    <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
      <line x1="19" y1="12" x2="5" y2="12"/><polyline points="12 19 5 12 12 5"/>
    </svg>
  ),
  Check: () => (
    <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2.5" strokeLinecap="round" strokeLinejoin="round">
      <polyline points="20 6 9 17 4 12"/>
    </svg>
  ),
};
 
// ─── Botão base ──────────────────────────────────────────────────
const Btn = ({ children, variant = "primary", onClick, type = "button", small, style: extraStyle }) => {
  const base = {
    display: "inline-flex", alignItems: "center", gap: 7,
    fontFamily: "var(--body)", fontWeight: 500, cursor: "pointer",
    border: "none", borderRadius: 10, transition: "var(--trans)",
    padding: small ? "7px 14px" : "11px 22px",
    fontSize: small ? 13 : 14,
    ...extraStyle,
  };
  const styles = {
    primary: { background: "var(--accent1)", color: "#fff" },
    ghost:   { background: "rgba(255,255,255,0.05)", color: "var(--text)", border: "1px solid var(--border)" },
    danger:  { background: "rgba(255,107,107,0.12)", color: "var(--accent3)", border: "1px solid rgba(255,107,107,.25)" },
    success: { background: "rgba(0,212,170,0.12)", color: "var(--accent2)", border: "1px solid rgba(0,212,170,.25)" },
  };
  return (
    <button type={type} onClick={onClick} style={{ ...base, ...styles[variant] }}
      onMouseEnter={e => e.currentTarget.style.filter = "brightness(1.15)"}
      onMouseLeave={e => e.currentTarget.style.filter = ""}
    >{children}</button>
  );
};
 
// ─── Modal de confirmação de exclusão ───────────────────────────
const DeleteModal = ({ open, name, onConfirm, onCancel }) => {
  if (!open) return null;
  return (
    <div style={{ position: "fixed", inset: 0, background: "rgba(0,0,0,.65)", zIndex: 1000, display: "flex", alignItems: "center", justifyContent: "center" }}>
      <div style={{ background: "var(--card)", border: "1px solid var(--border)", borderRadius: var(--radius), padding: 32, maxWidth: 380, width: "90%", animation: "fadeUp .2s" }}>
        <p style={{ fontFamily: "var(--heading)", fontSize: 18, marginBottom: 10 }}>Confirmar exclusão</p>
        <p style={{ color: "var(--muted)", fontSize: 14, marginBottom: 24 }}>
          Deseja realmente excluir <strong style={{ color: "var(--text)" }}>{name}</strong>? Esta ação não pode ser desfeita.
        </p>
        <div style={{ display: "flex", gap: 10, justifyContent: "flex-end" }}>
          <Btn variant="ghost" onClick={onCancel}>Cancelar</Btn>
          <Btn variant="danger" onClick={onConfirm}><Icon.Trash /> Excluir</Btn>
        </div>
      </div>
    </div>
  );
};
 
// ─── Toast de feedback ───────────────────────────────────────────
const Toast = ({ msg, type, onClose }) => {
  useEffect(() => { const t = setTimeout(onClose, 3200); return () => clearTimeout(t); }, []);
  if (!msg) return null;
  const color = type === "error" ? "var(--accent3)" : "var(--accent2)";
  return (
    <div style={{
      position: "fixed", bottom: 28, right: 28, zIndex: 2000,
      background: "var(--card)", border: `1px solid ${color}`,
      borderRadius: 12, padding: "13px 20px", fontSize: 14,
      display: "flex", alignItems: "center", gap: 10,
      animation: "fadeUp .25s", color,
    }}>
      {type !== "error" && <Icon.Check />}{msg}
    </div>
  );
};
 
// ─── Layout wrapper ──────────────────────────────────────────────
const Layout = ({ children, page, onNav }) => (
  <div style={{ minHeight: "100vh", display: "flex", flexDirection: "column" }}>
    {/* Navbar */}
    <header style={{
      position: "sticky", top: 0, zIndex: 100,
      background: "rgba(13,15,20,.85)", backdropFilter: "blur(12px)",
      borderBottom: "1px solid var(--border)",
      display: "flex", alignItems: "center", justifyContent: "space-between",
      padding: "0 36px", height: 62,
    }}>
      <span
        onClick={() => onNav("home")}
        style={{ fontFamily: "var(--heading)", fontWeight: 800, fontSize: 20, cursor: "pointer", letterSpacing: -0.5 }}
      >
        <span style={{ color: "var(--accent1)" }}>Loca</span>
        <span style={{ color: "var(--accent2)" }}>DDD</span>
      </span>
      <nav style={{ display: "flex", gap: 4 }}>
        {[
          { id: "clientes",  label: "Clientes" },
          { id: "veiculos",  label: "Veículos" },
          { id: "alocacoes", label: "Alocações" },
        ].map(item => (
          <button key={item.id} onClick={() => onNav(item.id)}
            style={{
              fontFamily: "var(--body)", fontSize: 13.5, fontWeight: 500,
              background: page === item.id ? "rgba(108,99,255,.15)" : "transparent",
              color: page === item.id ? "var(--accent1)" : "var(--muted)",
              border: page === item.id ? "1px solid rgba(108,99,255,.3)" : "1px solid transparent",
              borderRadius: 8, padding: "6px 14px", cursor: "pointer",
              transition: "var(--trans)",
            }}
          >{item.label}</button>
        ))}
      </nav>
    </header>
 
    {/* Content */}
    <main style={{ flex: 1, padding: "40px 36px", maxWidth: 1100, margin: "0 auto", width: "100%" }}>
      {children}
    </main>
  </div>
);
 
// ════════════════════════════════════════════════════════════════
//  HOME PAGE
// ════════════════════════════════════════════════════════════════
const HomePage = ({ onNav }) => {
  const cards = [
    {
      id: "clientes",
      icon: <Icon.User />,
      label: "Clientes",
      desc: "Cadastro de clientes, CNH, endereço e contato.",
      accent: "var(--accent1)",
      glow: "rgba(108,99,255,.25)",
    },
    {
      id: "veiculos",
      icon: <Icon.Car />,
      label: "Veículos",
      desc: "Frota disponível: modelo, marca, placa e grupo.",
      accent: "var(--accent2)",
      glow: "rgba(0,212,170,.25)",
    },
    {
      id: "alocacoes",
      icon: <Icon.Rental />,
      label: "Alocações",
      desc: "Controle de locações, datas e status de devolução.",
      accent: "var(--accent3)",
      glow: "rgba(255,107,107,.25)",
    },
  ];
 
  return (
    <div>
      {/* Hero */}
      <div style={{
        textAlign: "center", padding: "60px 0 56px",
        animation: "fadeUp .5s both",
      }}>
        {/* Decorative gradient blob */}
        <div style={{
          position: "absolute", left: "50%", top: 120,
          transform: "translateX(-50%)",
          width: 600, height: 280,
          background: "radial-gradient(ellipse, rgba(108,99,255,.18) 0%, transparent 70%)",
          pointerEvents: "none",
        }} />
        <p style={{ color: "var(--accent1)", fontFamily: "var(--heading)", fontWeight: 600, fontSize: 13, letterSpacing: 3, textTransform: "uppercase", marginBottom: 18 }}>
          Sistema de Locação de Veículos
        </p>
        <h1 style={{
          fontFamily: "var(--heading)", fontWeight: 800,
          fontSize: "clamp(36px, 5vw, 58px)", lineHeight: 1.1,
          letterSpacing: -1.5, marginBottom: 18,
        }}>
          Bem-vindo ao{" "}
          <span style={{ background: "linear-gradient(135deg, var(--accent1), var(--accent2))", WebkitBackgroundClip: "text", WebkitTextFillColor: "transparent" }}>
            LocaDDD
          </span>
        </h1>
        <p style={{ color: "var(--muted)", fontSize: 17, maxWidth: 460, margin: "0 auto 40px", lineHeight: 1.65 }}>
          Gerencie clientes, frota e alocações em um só lugar.
          Selecione um módulo para começar.
        </p>
      </div>
 
      {/* Cards */}
      <div style={{ display: "grid", gridTemplateColumns: "repeat(auto-fit, minmax(280px, 1fr))", gap: 24, animation: "fadeUp .6s .12s both" }}>
        {cards.map(({ id, icon, label, desc, accent, glow }) => (
          <button key={id} onClick={() => onNav(id)}
            style={{
              background: "var(--card)", border: "1px solid var(--border)",
              borderRadius: 18, padding: "36px 32px", cursor: "pointer",
              textAlign: "left", transition: "var(--trans)",
              position: "relative", overflow: "hidden",
            }}
            onMouseEnter={e => {
              e.currentTarget.style.border = `1px solid ${accent}55`;
              e.currentTarget.style.boxShadow = `0 0 32px ${glow}`;
              e.currentTarget.style.transform = "translateY(-3px)";
            }}
            onMouseLeave={e => {
              e.currentTarget.style.border = "1px solid var(--border)";
              e.currentTarget.style.boxShadow = "";
              e.currentTarget.style.transform = "";
            }}
          >
            {/* icon circle */}
            <div style={{
              width: 58, height: 58, borderRadius: 14,
              background: `${accent}18`, color: accent,
              display: "flex", alignItems: "center", justifyContent: "center",
              marginBottom: 22,
            }}>{icon}</div>
 
            <p style={{ fontFamily: "var(--heading)", fontWeight: 700, fontSize: 22, marginBottom: 9, color: "var(--text)" }}>{label}</p>
            <p style={{ color: "var(--muted)", fontSize: 14, lineHeight: 1.6 }}>{desc}</p>
 
            {/* decorative corner gradient */}
            <div style={{
              position: "absolute", bottom: -30, right: -30,
              width: 120, height: 120, borderRadius: "50%",
              background: `radial-gradient(circle, ${glow} 0%, transparent 70%)`,
              pointerEvents: "none",
            }} />
          </button>
        ))}
      </div>
    </div>
  );
};
 
// ════════════════════════════════════════════════════════════════
//  CRUD TABLE — componente genérico reutilizável pelas 3 entidades
// ════════════════════════════════════════════════════════════════
const CrudTable = ({ title, icon, accent, columns, rows, loading, onAdd, onEdit, onDelete }) => {
  const [deleteTarget, setDeleteTarget] = useState(null);
 
  return (
    <div style={{ animation: "fadeUp .4s both" }}>
      {/* Header */}
      <div style={{ display: "flex", alignItems: "center", justifyContent: "space-between", marginBottom: 28 }}>
        <div style={{ display: "flex", alignItems: "center", gap: 14 }}>
          <div style={{ color: accent, background: `${accent}18`, borderRadius: 12, padding: "9px 11px", display: "flex" }}>{icon}</div>
          <h2 style={{ fontFamily: "var(--heading)", fontSize: 26, fontWeight: 700, letterSpacing: -0.5 }}>{title}</h2>
        </div>
        <Btn onClick={onAdd}><Icon.Plus /> Novo registro</Btn>
      </div>
 
      {/* Table */}
      <div style={{ background: "var(--card)", border: "1px solid var(--border)", borderRadius: 16, overflow: "hidden" }}>
        <table style={{ width: "100%", borderCollapse: "collapse", fontSize: 14 }}>
          <thead>
            <tr style={{ background: "rgba(255,255,255,.03)", borderBottom: "1px solid var(--border)" }}>
              {columns.map(c => (
                <th key={c.key} style={{
                  padding: "14px 20px", textAlign: "left",
                  fontFamily: "var(--heading)", fontWeight: 600, fontSize: 12,
                  color: "var(--muted)", letterSpacing: 1, textTransform: "uppercase",
                }}>{c.label}</th>
              ))}
              <th style={{ padding: "14px 20px", width: 100 }} />
            </tr>
          </thead>
          <tbody>
            {loading && Array.from({ length: 4 }).map((_, i) => (
              <tr key={i} style={{ borderBottom: "1px solid var(--border)" }}>
                {[...columns, { key: "_" }].map((c, j) => (
                  <td key={j} style={{ padding: "16px 20px" }}>
                    <div className="skeleton" style={{ height: 18, width: j === columns.length ? 80 : "70%" }} />
                  </td>
                ))}
              </tr>
            ))}
            {!loading && rows.length === 0 && (
              <tr>
                <td colSpan={columns.length + 1} style={{ padding: 48, textAlign: "center", color: "var(--muted)", fontSize: 15 }}>
                  Nenhum registro encontrado.
                </td>
              </tr>
            )}
            {!loading && rows.map((row, i) => (
              <tr key={row.id ?? i}
                style={{ borderBottom: "1px solid var(--border)", transition: "var(--trans)" }}
                onMouseEnter={e => e.currentTarget.style.background = "rgba(255,255,255,.025)"}
                onMouseLeave={e => e.currentTarget.style.background = ""}
              >
                {columns.map(c => (
                  <td key={c.key} style={{ padding: "15px 20px", color: c.primary ? "var(--text)" : "var(--muted)" }}>
                    {c.render ? c.render(row[c.key], row) : row[c.key] ?? "—"}
                  </td>
                ))}
                <td style={{ padding: "15px 20px" }}>
                  <div style={{ display: "flex", gap: 6, justifyContent: "flex-end" }}>
                    <Btn small variant="ghost" onClick={() => onEdit(row)}><Icon.Edit /></Btn>
                    <Btn small variant="danger" onClick={() => setDeleteTarget(row)}><Icon.Trash /></Btn>
                  </div>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
 
      <DeleteModal
        open={!!deleteTarget}
        name={deleteTarget?.nome || deleteTarget?.modelo || deleteTarget?.id || "este registro"}
        onConfirm={() => { onDelete(deleteTarget); setDeleteTarget(null); }}
        onCancel={() => setDeleteTarget(null)}
      />
    </div>
  );
};
 
// ════════════════════════════════════════════════════════════════
//  FORM MODAL — abre sobre a tela para novo/editar
// ════════════════════════════════════════════════════════════════
const FormModal = ({ open, title, fields, initialData, onSave, onClose }) => {
  const [form, setForm] = useState({});
 
  useEffect(() => {
    setForm(initialData ?? {});
  }, [initialData, open]);
 
  if (!open) return null;
 
  const handleSubmit = e => {
    e.preventDefault();
    onSave(form);
  };
 
  return (
    <div style={{ position: "fixed", inset: 0, background: "rgba(0,0,0,.72)", zIndex: 900, display: "flex", alignItems: "center", justifyContent: "center", padding: 20 }}>
      <div style={{
        background: "var(--card)", border: "1px solid var(--border)",
        borderRadius: 20, padding: 36, width: "100%", maxWidth: 520,
        animation: "fadeUp .22s",
        maxHeight: "90vh", overflowY: "auto",
      }}>
        <div style={{ display: "flex", justifyContent: "space-between", alignItems: "center", marginBottom: 28 }}>
          <h3 style={{ fontFamily: "var(--heading)", fontSize: 20, fontWeight: 700 }}>{title}</h3>
          <button onClick={onClose} style={{ background: "none", border: "none", color: "var(--muted)", cursor: "pointer", fontSize: 22, lineHeight: 1 }}>×</button>
        </div>
 
        <form onSubmit={handleSubmit}>
          <div style={{ display: "grid", gridTemplateColumns: "1fr 1fr", gap: "18px 16px" }}>
            {fields.map(f => (
              <div key={f.key} style={{ gridColumn: f.full ? "1 / -1" : "auto" }}>
                <label style={{ display: "block", fontSize: 12, fontWeight: 500, color: "var(--muted)", marginBottom: 7, letterSpacing: .5, textTransform: "uppercase" }}>
                  {f.label}
                </label>
                {f.type === "select" ? (
                  <select
                    value={form[f.key] ?? ""}
                    onChange={e => setForm(p => ({ ...p, [f.key]: e.target.value }))}
                    style={inputStyle}
                  >
                    <option value="">Selecione...</option>
                    {f.options?.map(o => <option key={o.value} value={o.value}>{o.label}</option>)}
                  </select>
                ) : (
                  <input
                    type={f.type || "text"}
                    placeholder={f.placeholder}
                    value={form[f.key] ?? ""}
                    onChange={e => setForm(p => ({ ...p, [f.key]: e.target.value }))}
                    required={f.required}
                    style={inputStyle}
                  />
                )}
              </div>
            ))}
          </div>
          <div style={{ display: "flex", justifyContent: "flex-end", gap: 10, marginTop: 28 }}>
            <Btn variant="ghost" onClick={onClose} type="button">Cancelar</Btn>
            <Btn type="submit" variant="success"><Icon.Check /> Salvar</Btn>
          </div>
        </form>
      </div>
    </div>
  );
};
 
const inputStyle = {
  width: "100%", background: "rgba(255,255,255,.05)",
  border: "1px solid var(--border)", borderRadius: 9,
  padding: "10px 13px", color: "var(--text)",
  fontFamily: "var(--body)", fontSize: 14,
  outline: "none",
};
 
// ════════════════════════════════════════════════════════════════
//  CLIENTES PAGE
// ════════════════════════════════════════════════════════════════
const ClientesPage = () => {
  const [rows, setRows]       = useState([]);
  const [loading, setLoading] = useState(true);
  const [modal, setModal]     = useState({ open: false, data: null });
  const [toast, setToast]     = useState(null);
 
  // ── Simulação de chamada à API (substituir por fetch real) ──
  useEffect(() => {
    setTimeout(() => {
      setRows([
        { id: 1, nome: "Ana Paula Souza", cpf: "123.456.789-00", cnh: "98765", celular: "(11) 91234-5678", estado: true },
        { id: 2, nome: "Carlos Lima",     cpf: "987.654.321-00", cnh: "12345", celular: "(21) 99876-5432", estado: true },
      ]);
      setLoading(false);
    }, 900);
  }, []);
 
  const fields = [
    { key: "nome",                label: "Nome completo",       required: true,  full: true },
    { key: "cpf",                 label: "CPF",                 required: true,  placeholder: "000.000.000-00" },
    { key: "cnh",                 label: "CNH",                 required: true },
    { key: "celular",             label: "Celular",             placeholder: "(11) 9..." },
    { key: "cep",                 label: "CEP",                 placeholder: "00000-000" },
    { key: "endereco",            label: "Endereço",            full: true },
    { key: "complementoEndereco", label: "Complemento",         full: true },
    { key: "estado", label: "Ativo", type: "select",
      options: [{ value: "true", label: "Sim" }, { value: "false", label: "Não" }] },
  ];
 
  const columns = [
    { key: "nome",    label: "Nome",    primary: true },
    { key: "cpf",     label: "CPF" },
    { key: "cnh",     label: "CNH" },
    { key: "celular", label: "Celular" },
    { key: "estado",  label: "Status", render: v => (
      <span style={{ color: v ? "var(--accent2)" : "var(--accent3)", fontSize: 12, fontWeight: 600 }}>
        {v ? "● Ativo" : "○ Inativo"}
      </span>
    )},
  ];
 
  const handleSave = data => {
    // TODO: POST /api/Cliente  ou  PUT /api/Cliente/{id}
    if (data.id) {
      setRows(r => r.map(x => x.id === data.id ? { ...x, ...data } : x));
      setToast({ msg: "Cliente atualizado!", type: "success" });
    } else {
      const newRow = { ...data, id: Date.now() };
      setRows(r => [...r, newRow]);
      setToast({ msg: "Cliente criado!", type: "success" });
    }
    setModal({ open: false, data: null });
  };
 
  const handleDelete = row => {
    // TODO: DELETE /api/Cliente/{row.id}
    setRows(r => r.filter(x => x.id !== row.id));
    setToast({ msg: "Cliente excluído.", type: "success" });
  };
 
  return (
    <>
      <CrudTable
        title="Clientes" icon={<Icon.User />} accent="var(--accent1)"
        columns={columns} rows={rows} loading={loading}
        onAdd={() => setModal({ open: true, data: null })}
        onEdit={row => setModal({ open: true, data: row })}
        onDelete={handleDelete}
      />
      <FormModal
        open={modal.open}
        title={modal.data?.id ? "Editar Cliente" : "Novo Cliente"}
        fields={fields}
        initialData={modal.data}
        onSave={handleSave}
        onClose={() => setModal({ open: false, data: null })}
      />
      {toast && <Toast msg={toast.msg} type={toast.type} onClose={() => setToast(null)} />}
    </>
  );
};
 
// ════════════════════════════════════════════════════════════════
//  VEÍCULOS PAGE
// ════════════════════════════════════════════════════════════════
const grupoOpts = [
  { value: "0", label: "Econômico" },
  { value: "1", label: "Intermediário" },
  { value: "2", label: "Executivo" },
  { value: "3", label: "SUV" },
  { value: "4", label: "Premium" },
];
 
const VeiculosPage = () => {
  const [rows, setRows]       = useState([]);
  const [loading, setLoading] = useState(true);
  const [modal, setModal]     = useState({ open: false, data: null });
  const [toast, setToast]     = useState(null);
 
  useEffect(() => {
    setTimeout(() => {
      setRows([
        { id: 1, modelo: "Onix",    marca: "Chevrolet", placa: "ABC-1234", grupo: "0" },
        { id: 2, modelo: "Corolla", marca: "Toyota",    placa: "XYZ-5678", grupo: "2" },
      ]);
      setLoading(false);
    }, 900);
  }, []);
 
  const fields = [
    { key: "modelo", label: "Modelo", required: true },
    { key: "marca",  label: "Marca",  required: true },
    { key: "placa",  label: "Placa",  required: true, placeholder: "ABC-0000" },
    { key: "grupo",  label: "Grupo",  type: "select", options: grupoOpts },
  ];
 
  const columns = [
    { key: "modelo", label: "Modelo", primary: true },
    { key: "marca",  label: "Marca" },
    { key: "placa",  label: "Placa" },
    { key: "grupo",  label: "Grupo", render: v => grupoOpts.find(o => o.value === String(v))?.label ?? v },
  ];
 
  const handleSave = data => {
    // TODO: POST/PUT /api/Veiculo
    if (data.id) {
      setRows(r => r.map(x => x.id === data.id ? { ...x, ...data } : x));
      setToast({ msg: "Veículo atualizado!", type: "success" });
    } else {
      setRows(r => [...r, { ...data, id: Date.now() }]);
      setToast({ msg: "Veículo criado!", type: "success" });
    }
    setModal({ open: false, data: null });
  };
 
  const handleDelete = row => {
    // TODO: DELETE /api/Veiculo/{row.id}
    setRows(r => r.filter(x => x.id !== row.id));
    setToast({ msg: "Veículo excluído.", type: "success" });
  };
 
  return (
    <>
      <CrudTable
        title="Veículos" icon={<Icon.Car />} accent="var(--accent2)"
        columns={columns} rows={rows} loading={loading}
        onAdd={() => setModal({ open: true, data: null })}
        onEdit={row => setModal({ open: true, data: row })}
        onDelete={handleDelete}
      />
      <FormModal
        open={modal.open}
        title={modal.data?.id ? "Editar Veículo" : "Novo Veículo"}
        fields={fields}
        initialData={modal.data}
        onSave={handleSave}
        onClose={() => setModal({ open: false, data: null })}
      />
      {toast && <Toast msg={toast.msg} type={toast.type} onClose={() => setToast(null)} />}
    </>
  );
};
 
// ════════════════════════════════════════════════════════════════
//  ALOCAÇÕES PAGE
// ════════════════════════════════════════════════════════════════
const statusOpts = [
  { value: "0", label: "Reservado" },
  { value: "1", label: "Ativo" },
  { value: "2", label: "Devolvido" },
  { value: "3", label: "Cancelado" },
];
const statusColor = { "0": "#6c63ff", "1": "#00d4aa", "2": "#6b7280", "3": "#ff6b6b" };
 
const AlocacoesPage = () => {
  const [rows, setRows]       = useState([]);
  const [loading, setLoading] = useState(true);
  const [modal, setModal]     = useState({ open: false, data: null });
  const [toast, setToast]     = useState(null);
 
  useEffect(() => {
    setTimeout(() => {
      setRows([
        { id: 1, idCliente: 1, nomeCliente: "Ana Paula Souza", idCarro: 2, modeloCarro: "Corolla", dataInicio: "2025-04-20", dataDevolucao: "2025-04-25", dataFim: "", status: "1" },
        { id: 2, idCliente: 2, nomeCliente: "Carlos Lima",     idCarro: 1, modeloCarro: "Onix",    dataInicio: "2025-04-18", dataDevolucao: "2025-04-22", dataFim: "2025-04-22", status: "2" },
      ]);
      setLoading(false);
    }, 900);
  }, []);
 
  const fields = [
    { key: "idCliente",    label: "ID do Cliente",  required: true, type: "number" },
    { key: "idCarro",      label: "ID do Veículo",  required: true, type: "number" },
    { key: "dataInicio",   label: "Data de Início", required: true, type: "date" },
    { key: "dataDevolucao",label: "Previsão Devolução", required: true, type: "date" },
    { key: "dataFim",      label: "Data Fim Real",  type: "date" },
    { key: "status",       label: "Status",         type: "select", options: statusOpts },
  ];
 
  const columns = [
    { key: "nomeCliente",   label: "Cliente",   primary: true },
    { key: "modeloCarro",   label: "Veículo" },
    { key: "dataInicio",    label: "Início" },
    { key: "dataDevolucao", label: "Devolução" },
    { key: "status", label: "Status", render: v => (
      <span style={{ color: statusColor[v] ?? "var(--muted)", fontSize: 12, fontWeight: 600 }}>
        ● {statusOpts.find(o => o.value === String(v))?.label ?? v}
      </span>
    )},
  ];
 
  const handleSave = data => {
    // TODO: POST/PUT /api/Alocacao
    if (data.id) {
      setRows(r => r.map(x => x.id === data.id ? { ...x, ...data } : x));
      setToast({ msg: "Alocação atualizada!", type: "success" });
    } else {
      setRows(r => [...r, { ...data, id: Date.now() }]);
      setToast({ msg: "Alocação criada!", type: "success" });
    }
    setModal({ open: false, data: null });
  };
 
  const handleDelete = row => {
    // TODO: DELETE /api/Alocacao/{row.id}
    setRows(r => r.filter(x => x.id !== row.id));
    setToast({ msg: "Alocação excluída.", type: "success" });
  };
 
  return (
    <>
      <CrudTable
        title="Alocações" icon={<Icon.Rental />} accent="var(--accent3)"
        columns={columns} rows={rows} loading={loading}
        onAdd={() => setModal({ open: true, data: null })}
        onEdit={row => setModal({ open: true, data: row })}
        onDelete={handleDelete}
      />
      <FormModal
        open={modal.open}
        title={modal.data?.id ? "Editar Alocação" : "Nova Alocação"}
        fields={fields}
        initialData={modal.data}
        onSave={handleSave}
        onClose={() => setModal({ open: false, data: null })}
      />
      {toast && <Toast msg={toast.msg} type={toast.type} onClose={() => setToast(null)} />}
    </>
  );
};
 
// ════════════════════════════════════════════════════════════════
//  APP ROOT — roteamento simples por estado
// ════════════════════════════════════════════════════════════════
export default function App() {
  const [page, setPage] = useState("home");
 
  const pages = {
    home:      <HomePage onNav={setPage} />,
    clientes:  <ClientesPage />,
    veiculos:  <VeiculosPage />,
    alocacoes: <AlocacoesPage />,
  };
 
  return (
    <>
      <StyleTag />
      <Layout page={page} onNav={setPage}>
        {pages[page] ?? <HomePage onNav={setPage} />}
      </Layout>
    </>
  );
}
